"use client";

import { useEffect, useState } from "react";
import Sidebar from "@/components/Sidebar";
import { apiFetch } from "@/lib/api";

type StockItem = {
    id: number;
    name: string;
    quantity: number;
};

const toolNames = new Set(["Hammer", "Skrutrekker", "Vanlig verktøy"]);

export default function MaintenanceLagerPage() {
    const [items, setItems] = useState<StockItem[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [updatingItemId, setUpdatingItemId] = useState<number | null>(null);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        async function loadItems() {
            try {
                setError(null);

                const response = await apiFetch<StockItem[]>(
                    "/api/location-inventory/maintenance",
                );

                setItems(response);
            } catch {
                setError("Klarte ikke å hente lageret. Sjekk at backend kjører.");
            } finally {
                setIsLoading(false);
            }
        }

        void loadItems();
    }, []);

    async function updateQuantity(itemId: number, delta: -1 | 1) {
        try {
            setUpdatingItemId(itemId);
            setError(null);

            const updated = await apiFetch<StockItem>(
                `/api/location-inventory/maintenance/${itemId}/adjust`,
                {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({ delta }),
                },
            );

            setItems((current) =>
                current.map((item) =>
                    item.id === updated.id ? updated : item,
                ),
            );
        } catch {
            setError("Klarte ikke å lagre endringen.");
        } finally {
            setUpdatingItemId(null);
        }
    }

    return (
        <div className="min-h-screen bg-[#f4f6f8] md:flex">
            <Sidebar />

            <main className="min-w-0 flex-1">
                <header className="border-b border-slate-200 bg-white px-4 py-4 sm:px-6 lg:px-8">
                    <div className="mx-auto max-w-5xl">
                        <h1 className="text-xl font-semibold text-slate-950 sm:text-2xl">
                            Maintenance lager
                        </h1>

                        <p className="mt-1 text-sm text-slate-500">
                            Fokus på tilgjengelig utstyr
                        </p>
                    </div>
                </header>

                <div className="mx-auto max-w-5xl px-4 py-6 sm:px-6 lg:px-8">
                    {error ? (
                        <div className="mb-4 rounded-xl border border-rose-200 bg-rose-50 px-4 py-3 text-sm text-rose-700">
                            {error}
                        </div>
                    ) : null}

                    <section className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm">
                        <div className="grid grid-cols-[1.5fr_1fr_1.2fr] bg-slate-50 px-5 py-3 text-xs font-medium uppercase tracking-wide text-slate-500">
                            <span>Produkt</span>
                            <span>Antall</span>
                            <span>Juster</span>
                        </div>

                        {isLoading ? (
                            <div className="px-5 py-6 text-sm text-slate-500">
                                Henter lager...
                            </div>
                        ) : (
                            items.map((item) => {
                                const isOutOfStock = item.quantity === 0;
                                const isTool = toolNames.has(item.name);
                                const isUpdating = updatingItemId === item.id;

                                return (
                                    <div
                                        key={item.id}
                                        className={`grid grid-cols-[1.5fr_1fr_1.2fr] items-center border-t border-slate-100 px-5 py-4 text-sm ${isOutOfStock && isTool
                                                ? "bg-rose-50 text-rose-700"
                                                : "bg-white"
                                            }`}
                                    >
                                        <span>{item.name}</span>

                                        <span
                                            className={
                                                isOutOfStock && isTool
                                                    ? "font-semibold text-rose-700"
                                                    : "text-slate-700"
                                            }
                                        >
                                            {item.quantity}
                                        </span>

                                        <div className="flex gap-2">
                                            <button
                                                type="button"
                                                disabled={item.quantity <= 0 || isUpdating}
                                                onClick={() => void updateQuantity(item.id, -1)}
                                                className="h-8 w-8 rounded-lg border border-slate-300 bg-white text-slate-700 transition hover:bg-slate-100 disabled:cursor-not-allowed disabled:opacity-40"
                                            >
                                                −
                                            </button>

                                            <button
                                                type="button"
                                                disabled={isUpdating}
                                                onClick={() => void updateQuantity(item.id, 1)}
                                                className="h-8 w-8 rounded-lg border border-slate-300 bg-white text-slate-700 transition hover:bg-slate-100 disabled:cursor-not-allowed disabled:opacity-40"
                                            >
                                                +
                                            </button>
                                        </div>
                                    </div>
                                );
                            })
                        )}
                    </section>
                </div>
            </main>
        </div>
    );
}