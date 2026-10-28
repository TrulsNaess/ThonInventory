"use client";

import { useMemo, useState } from "react";
import CategoryCard from "@/components/CategoryCard";
import InventoryList from "@/components/InventoryList";
import Sidebar from "@/components/Sidebar";
import SummaryCards from "@/components/SummaryCards";
import type { InventoryResponse } from "@/types/inventory";

type Props = {
  data: InventoryResponse | null;
};

type StatusFilter = "all" | "missing" | "ordered";

export default function InventoryDashboard({ data }: Props) {
  const [query, setQuery] = useState("");
  const [categoryId, setCategoryId] = useState<number | null>(null);
  const [status, setStatus] = useState<StatusFilter>("all");
  const [isAlertsOpen, setIsAlertsOpen] = useState(false);

  const categories = data?.categories ?? [];
  const alerts = data?.alerts ?? [];

  const filteredCategories = useMemo(() => {
    const normalizedQuery = query.trim().toLocaleLowerCase("nb-NO");

    return categories
      .filter((category) => categoryId === null || category.id === categoryId)
      .map((category) => ({
        ...category,
        items: category.items.filter((item) => {
          const matchesQuery =
            normalizedQuery.length === 0 ||
            item.name.toLocaleLowerCase("nb-NO").includes(normalizedQuery) ||
            item.notes?.toLocaleLowerCase("nb-NO").includes(normalizedQuery);

          const matchesStatus =
            status === "all" ||
            (status === "missing" && item.isMissing) ||
            (status === "ordered" && item.isOrdered);

          return Boolean(matchesQuery && matchesStatus);
        }),
      }));
  }, [categories, categoryId, query, status]);

  return (
    <div className="min-h-screen bg-[#f4f6f8] md:flex">
      <Sidebar />

      <main className="min-w-0 flex-1">
        <header className="border-b border-slate-200 bg-white px-4 py-4 sm:px-6 lg:px-8">
          <div className="mx-auto flex max-w-7xl items-center justify-between gap-4">
            <div>
              <p className="text-xs font-semibold tracking-[0.2em] text-slate-400 md:hidden">THON HOTEL</p>
              <h1 className="text-xl font-semibold text-slate-950 sm:text-2xl">Lageroversikt</h1>
            </div>

            <div className="flex items-center gap-3">
              <button
                type="button"
                onClick={() => setIsAlertsOpen((current) => !current)}
                className="relative rounded-full border border-slate-200 bg-white px-3 py-1.5 text-sm font-medium text-slate-700 hover:bg-slate-50"
              >
                <span aria-hidden>🔔</span>
                {alerts.length > 0 ? (
                  <span className="absolute -right-1 -top-1 inline-flex min-h-5 min-w-5 items-center justify-center rounded-full bg-rose-500 px-1 text-[10px] font-semibold text-white">
                    {alerts.length}
                  </span>
                ) : null}
              </button>
              <span className="rounded-full bg-slate-100 px-3 py-1.5 text-xs font-medium text-slate-600">Resepsjonslager</span>
            </div>
          </div>
        </header>

        <div className="mx-auto max-w-7xl space-y-6 px-4 py-6 sm:px-6 lg:px-8 lg:py-8">
          {!data ? (
            <section className="rounded-2xl border border-rose-200 bg-rose-50 p-5 text-sm text-rose-800">
              <p className="font-medium">Klarte ikke å hente lagerdata.</p>
              <p className="mt-1 text-rose-700">Sjekk at serverdelen kjører på port 5003 og at databasetilkoblingen er riktig.</p>
            </section>
          ) : (
            <>
              {isAlertsOpen ? (
                <section className="rounded-2xl border border-amber-200 bg-amber-50 p-4 text-sm text-amber-900">
                  <h2 className="font-semibold">Varsler for lav beholdning</h2>
                  {alerts.length === 0 ? (
                    <p className="mt-2">Ingen varsler akkurat nå.</p>
                  ) : (
                    <ul className="mt-2 space-y-1">
                      {alerts.map((alert) => (
                        <li key={alert.itemId}>
                          {alert.itemName}: {alert.quantity} på lager (grense {alert.threshold})
                        </li>
                      ))}
                    </ul>
                  )}
                </section>
              ) : null}

              <SummaryCards
                total={data.summary.totalItems}
                totalQuantity={data.summary.totalQuantity}
                missing={data.summary.missingCount}
                ordered={data.summary.orderedCount}
                totalValue={data.summary.totalStockValue}
              />

              <section className="grid gap-3 sm:grid-cols-2 xl:grid-cols-4">
                {categories.map((category) => (
                  <CategoryCard
                    key={category.id}
                    name={category.name}
                    count={category.items.length}
                    missing={category.items.filter((item) => item.isMissing).length}
                    active={categoryId === category.id}
                    onClick={() => setCategoryId((current) => (current === category.id ? null : category.id))}
                  />
                ))}
              </section>

              <section className="rounded-2xl border border-slate-200 bg-white p-4 shadow-sm sm:p-5">
                <div className="flex flex-col gap-3 lg:flex-row lg:items-center lg:justify-between">
                  <div>
                    <h2 className="font-semibold text-slate-900">Varer</h2>
                    <p className="mt-0.5 text-xs text-slate-500">Søk eller filtrer lageroversikten</p>
                  </div>

                  <div className="flex flex-col gap-3 sm:flex-row">
                    <input
                      value={query}
                      onChange={(event) => setQuery(event.target.value)}
                      placeholder="Søk etter vare eller notat"
                      className="min-w-0 rounded-lg border border-slate-300 bg-white px-3 py-2 text-sm text-slate-900 outline-none transition placeholder:text-slate-400 focus:border-slate-500 sm:w-64"
                    />
                    <div className="flex rounded-lg bg-slate-100 p-1 text-xs font-medium">
                      {([
                        ["all", "Alle"],
                        ["missing", "Mangler"],
                        ["ordered", "Bestilt"],
                      ] as const).map(([value, label]) => (
                        <button
                          key={value}
                          type="button"
                          onClick={() => setStatus(value)}
                          className={`rounded-md px-3 py-1.5 transition ${
                            status === value ? "bg-white text-slate-900 shadow-sm" : "text-slate-500 hover:text-slate-700"
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
