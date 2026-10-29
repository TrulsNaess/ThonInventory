"use client";

import { useMemo, useState } from "react";
import Link from "next/link";
import CategoryCard from "@/components/CategoryCard";
import InventoryList from "@/components/InventoryList";
import Sidebar from "@/components/Sidebar";
import SummaryCards from "@/components/SummaryCards";
import { apiPost } from "@/lib/api";
import type {
    InventoryResponse,
    RegisterInventoryChangeRequest,
    InventoryItem,
} from "@/types/inventory";

type Props = {
    data: InventoryResponse | null;
};

type StatusFilter = "all" | "outofstock" | "discrepancy" | "ordered";

export default function InventoryDashboard({ data }: Props) {
    const [query, setQuery] = useState("");
    const [categoryId, setCategoryId] = useState<number | null>(null);
    const [status, setStatus] = useState<StatusFilter>("all");
    const [isAlertsOpen, setIsAlertsOpen] = useState(false);

    const [restockItemId, setRestockItemId] = useState<number>(0);
    const [restockQty, setRestockQty] = useState<number>(1);
    const [shrinkItemId, setShrinkItemId] = useState<number>(0);
    const [shrinkQty, setShrinkQty] = useState<number>(1);

    const categories = data?.categories ?? [];
    const alerts = data?.alerts ?? [];
    const allItems = categories.flatMap((category) => category.items);

    const selectedCategory = categories.find(
        (category) => category.id === categoryId,
    );

    async function submitInventoryChange(
        path: "/api/sales/restock" | "/api/sales/shrinkage",
        payload: RegisterInventoryChangeRequest,
    ) {
        await apiPost(path, payload);
        window.location.reload();
    }

    const filteredCategories = useMemo(() => {
        const normalizedQuery = query.trim().toLocaleLowerCase("nb-NO");

        return categories
            .filter(
                (category) => categoryId === null || category.id === categoryId,
            )
            .map((category) => ({
                ...category,
                items: category.items.filter((item) => {
                    const matchesQuery =
                        normalizedQuery.length === 0 ||
                        item.name
                            .toLocaleLowerCase("nb-NO")
                            .includes(normalizedQuery) ||
                        item.notes
                            ?.toLocaleLowerCase("nb-NO")
                            .includes(normalizedQuery);

                    const matchesStatus =
                        status === "all" ||
                        (status === "outofstock" && item.isOutOfStock) ||
                        (status === "discrepancy" && item.isDiscrepancy) ||
                        (status === "ordered" && item.isOrdered);

                    return Boolean(matchesQuery && matchesStatus);
                }),
            }));
    }, [categories, categoryId, query, status]);

    return (
        <div className="min-h-screen bg-gradient-to-br from-slate-50 via-[#f7f9fc] to-blue-50/70 md:flex">
            <Sidebar />

            <main className="min-w-0 flex-1">
                <header className="sticky top-0 z-30 border-b border-[#E7D6D9] bg-[#FAF3F4] px-4 py-4 shadow-sm backdrop-blur-xl sm:px-6 lg:px-8">
                    <div className="mx-auto flex max-w-7xl items-center justify-between gap-4">
                        <div>
                            <p className="text-xs font-semibold uppercase tracking-[0.18em] text-[#B8323E]">
                                Thon Hotel Kristiansand
                            </p>

                            <h1 className="mt-1 text-xl font-bold tracking-tight text-slate-950 sm:text-2xl">
                                Lageroversikt
                            </h1>
                        </div>

                        <div className="flex items-center gap-3">
                            <Link
                                href="/demo-kasse"
                                className="hidden rounded-xl border border-slate-200 bg-white px-4 py-2 text-xs font-semibold text-slate-700 shadow-sm transition hover:-translate-y-0.5 hover:border-blue-300 hover:shadow-md sm:block"
                            >
                                Demo-Kasse
                            </Link>

                            <button
                                type="button"
                                onClick={() => setIsAlertsOpen((current) => !current)}
                                className="relative flex h-10 w-10 items-center justify-center rounded-full border border-slate-200 bg-white shadow-sm transition hover:scale-105 hover:bg-slate-50"
                            >
                                <span aria-hidden>🔔</span>

                                {alerts.length > 0 ? (
                                    <span className="absolute -right-1 -top-1 inline-flex min-h-5 min-w-5 items-center justify-center rounded-full bg-rose-500 px-1 text-[10px] font-semibold text-white shadow">
                                        {alerts.length}
                                    </span>
                                ) : null}
                            </button>

                            <span className="rounded-full bg-[#eef4fb] px-3 py-2 text-xs font-semibold text-slate-600">
                                Resepsjonslager
                            </span>
                        </div>
                    </div>
                </header>

                <div className="mx-auto max-w-7xl space-y-7 px-4 py-6 sm:px-6 lg:px-8 lg:py-8">
                    {!data ? (
                        <section className="rounded-2xl border border-rose-200 bg-rose-50 p-5 text-sm text-rose-800 shadow-sm">
                            <p className="font-semibold">
                                Klarte ikke å hente lagerdata.
                            </p>
                            <p className="mt-1 text-rose-700">
                                Sjekk at serverdelen kjører på port 5003 og at
                                databasetilkoblingen er riktig.
                            </p>
                        </section>
                    ) : (
                        <>
                            {isAlertsOpen ? (
                                <section className="rounded-2xl border border-amber-200 bg-gradient-to-r from-amber-50 to-orange-50 p-5 text-sm text-amber-900 shadow-sm">
                                    <h2 className="font-semibold">
                                        Varsler for lav beholdning
                                    </h2>

                                    {alerts.length === 0 ? (
                                        <p className="mt-2">
                                            Ingen varsler akkurat nå.
                                        </p>
                                    ) : (
                                        <ul className="mt-3 space-y-2">
                                            {alerts.map((alert) => (
                                                <li
                                                    key={alert.itemId}
                                                    className="rounded-xl bg-white/70 px-3 py-2"
                                                >
                                                    <span className="font-medium">
                                                        {alert.itemName}
                                                    </span>
                                                    : {alert.quantity} på lager (grense{" "}
                                                    {alert.threshold})
                                                </li>
                                            ))}
                                        </ul>
                                    )}
                                </section>
                            ) : null}

                            <SummaryCards
                                total={data.summary.totalItems}
                                totalQuantity={data.summary.totalQuantity}
                                outOfStock={data.summary.outOfStockCount}
                                discrepancy={data.summary.discrepancyCount}
                                ordered={data.summary.orderedCount}
                                totalValue={data.summary.totalStockValue}
                            />

                            <section>
                                <div className="mb-3 flex items-end justify-between gap-4">
                                    <div>
                                        <h2 className="text-lg font-bold text-slate-950">
                                            Kategorier
                                        </h2>

                                        <p className="mt-1 text-sm text-slate-500">
                                            Velg en kategori for å filtrere varene
                                        </p>
                                    </div>

                                    {selectedCategory ? (
                                        <button
                                            type="button"
                                            onClick={() => setCategoryId(null)}
                                            className="rounded-lg bg-white px-3 py-2 text-xs font-semibold text-slate-600 shadow-sm ring-1 ring-slate-200 transition hover:bg-slate-50 hover:text-slate-900"
                                        >
                                            Vis alle varer
                                        </button>
                                    ) : null}
                                </div>

                                <div className="grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
                                    {categories.map((category) => (
                                        <CategoryCard
                                            key={category.id}
                                            name={category.name}
                                            count={category.items.length}
                                            missing={
                                                category.items.filter(
                                                    (item) => item.isOutOfStock,
                                                ).length
                                            }
                                            active={categoryId === category.id}
                                            onClick={() =>
                                                setCategoryId((current) =>
                                                    current === category.id
                                                        ? null
                                                        : category.id,
                                                )
                                            }
                                        />
                                    ))}
                                </div>
                            </section>

                            <section className="grid gap-4 lg:grid-cols-2">
                                <form
                                    className="rounded-2xl border border-emerald-200 bg-gradient-to-br from-emerald-50 to-white p-5 shadow-sm transition hover:shadow-md"
                                    onSubmit={(event) => {
                                        event.preventDefault();

                                        if (restockItemId > 0) {
                                            void submitInventoryChange(
                                                "/api/sales/restock",
                                                {
                                                    itemId: restockItemId,
                                                    quantity: restockQty,
                                                },
                                            );
                                        }
                                    }}
                                >
                                    <div className="mb-4">
                                        <p className="text-xs font-semibold uppercase tracking-wider text-emerald-600">
                                            Lager
                                        </p>
                                        <h3 className="mt-1 font-bold text-slate-950">
                                            Registrer påfyll
                                        </h3>
                                        <p className="mt-1 text-xs text-slate-500">
                                            Legg til nye varer i beholdningen
                                        </p>
                                    </div>

                                    <div className="flex flex-col gap-2 sm:flex-row">
                                        <select
                                            className="min-w-0 flex-1 rounded-xl border border-emerald-200 bg-white px-3 py-2.5 text-sm outline-none transition focus:border-emerald-400 focus:ring-2 focus:ring-emerald-100"
                                            value={restockItemId}
                                            onChange={(event) =>
                                                setRestockItemId(
                                                    Number(event.target.value),
                                                )
                                            }
                                        >
                                            <option value={0}>Velg vare</option>

                                            {allItems.map((item: InventoryItem) => (
                                                <option key={item.id} value={item.id}>
                                                    {item.name}
                                                </option>
                                            ))}
                                        </select>

                                        <input
                                            className="w-full rounded-xl border border-emerald-200 bg-white px-3 py-2.5 text-sm outline-none transition focus:border-emerald-400 focus:ring-2 focus:ring-emerald-100 sm:w-24"
                                            min={1}
                                            type="number"
                                            value={restockQty}
                                            onChange={(event) =>
                                                setRestockQty(Number(event.target.value))
                                            }
                                        />

                                        <button
                                            type="submit"
                                            className="rounded-xl bg-emerald-600 px-5 py-2.5 text-sm font-semibold text-white shadow-sm transition hover:-translate-y-0.5 hover:bg-emerald-700 hover:shadow-md"
                                        >
                                            Påfyll
                                        </button>
                                    </div>
                                </form>

                                <form
                                    className="rounded-2xl border border-rose-200 bg-gradient-to-br from-rose-50 to-white p-5 shadow-sm transition hover:shadow-md"
                                    onSubmit={(event) => {
                                        event.preventDefault();

                                        if (shrinkItemId > 0) {
                                            void submitInventoryChange(
                                                "/api/sales/shrinkage",
                                                {
                                                    itemId: shrinkItemId,
                                                    quantity: shrinkQty,
                                                    note: "Svinn",
                                                },
                                            );
                                        }
                                    }}
                                >
                                    <div className="mb-4">
                                        <p className="text-xs font-semibold uppercase tracking-wider text-rose-600">
                                            Beholdning
                                        </p>
                                        <h3 className="mt-1 font-bold text-slate-950">
                                            Registrer svinn
                                        </h3>
                                        <p className="mt-1 text-xs text-slate-500">
                                            Trekk varer fra lagerbeholdningen
                                        </p>
                                    </div>

                                    <div className="flex flex-col gap-2 sm:flex-row">
                                        <select
                                            className="min-w-0 flex-1 rounded-xl border border-rose-200 bg-white px-3 py-2.5 text-sm outline-none transition focus:border-rose-400 focus:ring-2 focus:ring-rose-100"
                                            value={shrinkItemId}
                                            onChange={(event) =>
                                                setShrinkItemId(
                                                    Number(event.target.value),
                                                )
                                            }
                                        >
                                            <option value={0}>Velg vare</option>

                                            {allItems.map((item: InventoryItem) => (
                                                <option key={item.id} value={item.id}>
                                                    {item.name}
                                                </option>
                                            ))}
                                        </select>

                                        <input
                                            className="w-full rounded-xl border border-rose-200 bg-white px-3 py-2.5 text-sm outline-none transition focus:border-rose-400 focus:ring-2 focus:ring-rose-100 sm:w-24"
                                            min={1}
                                            type="number"
                                            value={shrinkQty}
                                            onChange={(event) =>
                                                setShrinkQty(Number(event.target.value))
                                            }
                                        />

                                        <button
                                            type="submit"
                                            className="rounded-xl bg-rose-600 px-5 py-2.5 text-sm font-semibold text-white shadow-sm transition hover:-translate-y-0.5 hover:bg-rose-700 hover:shadow-md"
                                        >
                                            Svinn
                                        </button>
                                    </div>
                                </form>
                            </section>

                            <section className="rounded-2xl border border-slate-200 bg-white p-4 shadow-sm sm:p-5">
                                <div className="flex flex-col gap-3 lg:flex-row lg:items-center lg:justify-between">
                                    <div>
                                        <h2 className="font-bold text-slate-950">
                                            Varer
                                        </h2>
                                        <p className="mt-0.5 text-xs text-slate-500">
                                            Søk eller filtrer lageroversikten
                                        </p>
                                    </div>

                                    <div className="flex flex-col gap-3 sm:flex-row">
                                        <input
                                            value={query}
                                            onChange={(event) =>
                                                setQuery(event.target.value)
                                            }
                                            placeholder="Søk etter vare eller notat"
                                            className="min-w-0 rounded-xl border border-slate-300 bg-slate-50 px-4 py-2.5 text-sm text-slate-900 outline-none transition placeholder:text-slate-400 focus:border-blue-400 focus:bg-white focus:ring-2 focus:ring-blue-100 sm:w-72"
                                        />

                                        <div className="flex rounded-xl bg-slate-100 p-1 text-xs font-medium">
                                            {(
                                                [
                                                    ["all", "Alle"],
                                                    ["outofstock", "Utsolgt"],
                                                    ["discrepancy", "Avvik"],
                                                    ["ordered", "Bestilt"],
                                                ] as const
                                            ).map(([value, label]) => (
                                                <button
                                                    key={value}
                                                    type="button"
                                                    onClick={() => setStatus(value)}
                                                    className={`rounded-lg px-3 py-2 transition-all ${status === value
                                                            ? "bg-white font-semibold text-slate-950 shadow-sm"
                                                            : "text-slate-500 hover:bg-white/70 hover:text-slate-800"
                                                        }`}
                                                >
                                                    {label}
                                                </button>
                                            ))}
                                        </div>
                                    </div>
                                </div>
                            </section>

                            <InventoryList categories={filteredCategories} />
                        </>
                    )}
                </div>
            </main>
        </div>
    );
}