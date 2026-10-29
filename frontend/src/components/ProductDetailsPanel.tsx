"use client";

import { useEffect, useState } from "react";
import {
    apiFetch,
    apiPost,
    buildSalesLogPath,
} from "@/lib/api";
import type {
    InventoryItem,
    RegisterInventoryChangeRequest,
    SalesLogEntry,
    SalesLogResponse,
} from "@/types/inventory";

type Props = {
    item: InventoryItem;
    categoryName: string;
    imageSrc: string;
    onClose: () => void;
};

type ActionMode = "restock" | "shrinkage" | null;

function formatNok(value: number) {
    if (value === 0) return "Gratis for medlemmer";

    return new Intl.NumberFormat("nb-NO", {
        style: "currency",
        currency: "NOK",
        maximumFractionDigits: 0,
    }).format(value);
}

function formatDate(value: string | null) {
    if (!value) return "-";

    return new Intl.DateTimeFormat("nb-NO", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
    }).format(new Date(value));
}

function formatDateTime(value: string) {
    return new Intl.DateTimeFormat("nb-NO", {
        day: "2-digit",
        month: "2-digit",
        hour: "2-digit",
        minute: "2-digit",
    }).format(new Date(value));
}

function getStatus(item: InventoryItem) {
    if (item.isOutOfStock) {
        return {
            label: "Utsolgt",
            className:
                "bg-rose-50 text-rose-700 border-rose-100",
        };
    }

    if (item.isDiscrepancy) {
        return {
            label: "Avvik",
            className:
                "bg-violet-50 text-violet-700 border-violet-100",
        };
    }

    return {
        label: "På lager",
        className:
            "bg-emerald-50 text-emerald-700 border-emerald-100",
    };
}

function activityLabel(entry: SalesLogEntry) {
    if (entry.type === "restock") return "Påfyll";
    if (entry.type === "shrinkage") return "Svinn";
    return "Salg";
}

function activityAmount(entry: SalesLogEntry) {
    if (entry.type === "restock") {
        return `+${entry.quantity}`;
    }

    return `-${entry.quantity}`;
}

export default function ProductDetailsPanel({
    item,
    categoryName,
    imageSrc,
    onClose,
}: Props) {
    const [actionMode, setActionMode] =
        useState<ActionMode>(null);

    const [quantity, setQuantity] = useState(1);
    const [isSubmitting, setIsSubmitting] = useState(false);

    const [activities, setActivities] = useState<
        SalesLogEntry[]
    >([]);

    const [activityLoading, setActivityLoading] =
        useState(true);

    const [error, setError] = useState<string | null>(
        null,
    );

    const status = getStatus(item);

    useEffect(() => {
        function handleKeyDown(event: KeyboardEvent) {
            if (event.key === "Escape") {
                onClose();
            }
        }

        document.addEventListener("keydown", handleKeyDown);

        const previousOverflow = document.body.style.overflow;
        document.body.style.overflow = "hidden";

        return () => {
            document.removeEventListener(
                "keydown",
                handleKeyDown,
            );

            document.body.style.overflow = previousOverflow;
        };
    }, [onClose]);

    useEffect(() => {
        let cancelled = false;

        async function loadActivity() {
            setActivityLoading(true);

            try {
                const data =
                    await apiFetch<SalesLogResponse>(
                        buildSalesLogPath("month", "all"),
                    );

                if (!cancelled) {
                    setActivities(
                        data.entries
                            .filter(
                                (entry) => entry.itemId === item.id,
                            )
                            .slice(0, 5),
                    );
                }
            } catch {
                if (!cancelled) {
                    setActivities([]);
                }
            } finally {
                if (!cancelled) {
                    setActivityLoading(false);
                }
            }
        }

        void loadActivity();

        return () => {
            cancelled = true;
        };
    }, [item.id]);

    async function submitAction() {
        if (!actionMode || quantity <= 0) return;

        if (
            actionMode === "shrinkage" &&
            quantity > item.quantity
        ) {
            setError(
                `Du kan ikke registrere mer svinn enn de ${item.quantity} enhetene som er på lager.`,
            );
            return;
        }

        setError(null);
        setIsSubmitting(true);

        const path =
            actionMode === "restock"
                ? "/api/sales/restock"
                : "/api/sales/shrinkage";

        const body: RegisterInventoryChangeRequest = {
            itemId: item.id,
            quantity,
            note:
                actionMode === "restock"
                    ? "Påfyll registrert fra vareoversikt"
                    : "Svinn registrert fra vareoversikt",
        };

        try {
            await apiPost<SalesLogEntry, RegisterInventoryChangeRequest>(
                path,
                body,
            );

            window.location.reload();
        } catch {
            setError(
                "Handlingen kunne ikke registreres. Prøv igjen.",
            );

            setIsSubmitting(false);
        }
    }

    return (
        <div className="fixed inset-0 z-50">
            <button
                type="button"
                aria-label="Lukk produktdetaljer"
                onClick={onClose}
                className="absolute inset-0 bg-slate-950/20 backdrop-blur-[1px]"
            />

            <aside className="absolute right-0 top-0 flex h-full w-full max-w-[460px] flex-col border-l border-slate-200 bg-white shadow-2xl">
                <div className="flex items-start justify-between border-b border-slate-200 px-6 py-5">
                    <div className="flex min-w-0 items-center gap-4">
                        {imageSrc ? (
                            <div className="flex h-16 w-16 shrink-0 items-center justify-center overflow-hidden rounded-2xl border border-slate-200 bg-white p-1.5">
                                <img
                                    src={imageSrc}
                                    alt={item.name}
                                    className="h-full w-full object-contain"
                                />
                            </div>
                        ) : (
                            <div className="flex h-16 w-16 shrink-0 items-center justify-center rounded-2xl border border-slate-200 bg-slate-50 text-xs font-medium text-slate-400">
                                Ingen
                            </div>
                        )}

                        <div className="min-w-0">
                            <p className="text-xs font-semibold uppercase tracking-[0.16em] text-slate-400">
                                {categoryName}
                            </p>

                            <h2 className="mt-1 truncate text-xl font-semibold text-slate-950">
                                {item.name}
                            </h2>

                            <span
                                className={`mt-2 inline-flex rounded-full border px-2.5 py-1 text-xs font-medium ${status.className}`}
                            >
                                {status.label}
                            </span>
                        </div>
                    </div>

                    <button
                        type="button"
                        onClick={onClose}
                        className="ml-4 flex h-9 w-9 shrink-0 items-center justify-center rounded-full text-xl text-slate-400 transition hover:bg-slate-100 hover:text-slate-700"
                    >
                        ×
                    </button>
                </div>

                <div className="flex-1 overflow-y-auto">
                    <div className="border-b border-slate-100 px-6 py-6">
                        <p className="mb-4 text-xs font-semibold uppercase tracking-[0.14em] text-slate-400">
                            Lagerinformasjon
                        </p>

                        <div className="grid grid-cols-2 gap-x-8 gap-y-5">
                            <div>
                                <p className="text-xs text-slate-400">
                                    Antall på lager
                                </p>

                                <p className="mt-1 text-lg font-semibold text-slate-950">
                                    {item.quantity}
                                </p>
                            </div>

                            <div>
                                <p className="text-xs text-slate-400">
                                    Pris
                                </p>

                                <p className="mt-1 text-lg font-semibold text-slate-950">
                                    {formatNok(item.price)}
                                </p>
                            </div>

                            <div>
                                <p className="text-xs text-slate-400">
                                    Lagerverdi
                                </p>

                                <p className="mt-1 font-medium text-slate-800">
                                    {item.price === 0
                                        ? "-"
                                        : formatNok(item.stockValue)}
                                </p>
                            </div>

                            <div>
                                <p className="text-xs text-slate-400">
                                    Bestilling
                                </p>

                                <p className="mt-1 font-medium text-slate-800">
                                    {item.isOrdered ? "Bestilt" : "Ikke bestilt"}
                                </p>
                            </div>

                            <div>
                                <p className="text-xs text-slate-400">
                                    Mangler siden
                                </p>

                                <p className="mt-1 font-medium text-slate-800">
                                    {formatDate(item.missingSinceDate)}
                                </p>
                            </div>

                            <div>
                                <p className="text-xs text-slate-400">
                                    Forventet levering
                                </p>

                                <p className="mt-1 font-medium text-slate-800">
                                    {formatDate(
                                        item.expectedDeliveryDate,
                                    )}
                                </p>
                            </div>
                        </div>

                        {item.notes ? (
                            <div className="mt-5 rounded-xl bg-slate-50 px-4 py-3">
                                <p className="text-xs font-medium text-slate-400">
                                    Notat
                                </p>

                                <p className="mt-1 text-sm text-slate-700">
                                    {item.notes}
                                </p>
                            </div>
                        ) : null}
                    </div>

                    <div className="border-b border-slate-100 px-6 py-6">
                        <p className="mb-4 text-xs font-semibold uppercase tracking-[0.14em] text-slate-400">
                            Handlinger
                        </p>

                        <div className="grid grid-cols-2 gap-3">
                            <button
                                type="button"
                                onClick={() => {
                                    setQuantity(1);
                                    setError(null);

                                    setActionMode((current) =>
                                        current === "restock"
                                            ? null
                                            : "restock",
                                    );
                                }}
                                className={`rounded-xl border px-4 py-3 text-left transition ${actionMode === "restock"
                                        ? "border-emerald-300 bg-emerald-50"
                                        : "border-slate-200 bg-white hover:border-emerald-200 hover:bg-emerald-50/50"
                                    }`}
                            >
                                <span className="block text-sm font-semibold text-slate-900">
                                    Fyll på
                                </span>

                                <span className="mt-1 block text-xs text-slate-500">
                                    Legg til beholdning
                                </span>
                            </button>

                            <button
                                type="button"
                                disabled={item.quantity === 0}
                                onClick={() => {
                                    setQuantity(1);
                                    setError(null);

                                    setActionMode((current) =>
                                        current === "shrinkage"
                                            ? null
                                            : "shrinkage",
                                    );
                                }}
                                className={`rounded-xl border px-4 py-3 text-left transition disabled:cursor-not-allowed disabled:opacity-40 ${actionMode === "shrinkage"
                                        ? "border-rose-300 bg-rose-50"
                                        : "border-slate-200 bg-white hover:border-rose-200 hover:bg-rose-50/50"
                                    }`}
                            >
                                <span className="block text-sm font-semibold text-slate-900">
                                    Registrer svinn
                                </span>

                                <span className="mt-1 block text-xs text-slate-500">
                                    Trekk fra beholdning
                                </span>
                            </button>
                        </div>

                        {actionMode ? (
                            <div className="mt-4 rounded-xl border border-slate-200 bg-slate-50 p-4">
                                <label className="text-xs font-medium text-slate-600">
                                    {actionMode === "restock"
                                        ? "Antall som skal fylles på"
                                        : "Antall som skal registreres som svinn"}
                                </label>

                                <div className="mt-2 flex gap-2">
                                    <input
                                        type="number"
                                        min={1}
                                        max={
                                            actionMode === "shrinkage"
                                                ? item.quantity
                                                : undefined
                                        }
                                        value={quantity}
                                        onChange={(event) =>
                                            setQuantity(
                                                Number(event.target.value),
                                            )
                                        }
                                        className="min-w-0 flex-1 rounded-lg border border-slate-300 bg-white px-3 py-2 text-sm outline-none focus:border-slate-500"
                                    />

                                    <button
                                        type="button"
                                        disabled={
                                            isSubmitting || quantity <= 0
                                        }
                                        onClick={() => void submitAction()}
                                        className={`rounded-lg px-4 py-2 text-sm font-semibold text-white transition disabled:opacity-50 ${actionMode === "restock"
                                                ? "bg-emerald-600 hover:bg-emerald-700"
                                                : "bg-rose-600 hover:bg-rose-700"
                                            }`}
                                    >
                                        {isSubmitting
                                            ? "Lagrer..."
                                            : "Bekreft"}
                                    </button>
                                </div>

                                {error ? (
                                    <p className="mt-2 text-xs text-rose-600">
                                        {error}
                                    </p>
                                ) : null}
                            </div>
                        ) : null}
                    </div>

                    <div className="px-6 py-6">
                        <div className="mb-4 flex items-center justify-between">
                            <p className="text-xs font-semibold uppercase tracking-[0.14em] text-slate-400">
                                Siste aktivitet
                            </p>

                            <span className="text-xs text-slate-400">
                                Denne måneden
                            </span>
                        </div>

                        {activityLoading ? (
                            <p className="text-sm text-slate-400">
                                Henter aktivitet...
                            </p>
                        ) : activities.length === 0 ? (
                            <div className="rounded-xl border border-dashed border-slate-200 px-4 py-5 text-center text-sm text-slate-400">
                                Ingen aktivitet registrert denne måneden.
                            </div>
                        ) : (
                            <div className="space-y-1">
                                {activities.map((entry) => (
                                    <div
                                        key={entry.id}
                                        className="flex items-center justify-between border-b border-slate-100 py-3 last:border-0"
                                    >
                                        <div>
                                            <p className="text-sm font-medium text-slate-800">
                                                {activityLabel(entry)}
                                            </p>

                                            <p className="mt-0.5 text-xs text-slate-400">
                                                {formatDateTime(
                                                    entry.createdAt,
                                                )}
                                                {entry.note
                                                    ? ` · ${entry.note}`
                                                    : ""}
                                            </p>
                                        </div>

                                        <span
                                            className={`text-sm font-semibold ${entry.type === "restock"
                                                    ? "text-emerald-600"
                                                    : entry.type ===
                                                        "shrinkage"
                                                        ? "text-rose-600"
                                                        : "text-slate-700"
                                                }`}
                                        >
                                            {activityAmount(entry)}
                                        </span>
                                    </div>
                                ))}
                            </div>
                        )}
                    </div>
                </div>
            </aside>
        </div>
    );
}