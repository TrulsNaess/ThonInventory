import Sidebar from "@/components/Sidebar";
import { apiFetch, buildSalesLogPath } from "@/lib/api";
import type { SalesLogResponse } from "@/types/inventory";
import Link from "next/link";

const periodOptions = [
  { value: "today", label: "I dag" },
  { value: "week", label: "Uke" },
  { value: "month", label: "Måned" },
] as const;

const typeOptions = [
  { value: "all", label: "Alle" },
  { value: "sale", label: "Salg" },
  { value: "shrinkage", label: "Svinn" },
  { value: "restock", label: "Påfyll" },
] as const;

type PeriodValue = (typeof periodOptions)[number]["value"];
type TypeValue = (typeof typeOptions)[number]["value"];

function formatNok(value: number) {
  return new Intl.NumberFormat("nb-NO", {
    style: "currency",
    currency: "NOK",
    maximumFractionDigits: 0,
  }).format(value);
}

function formatDate(value: string) {
  return new Intl.DateTimeFormat("nb-NO", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
  }).format(new Date(value));
}

export default async function SalesLogPage({
  searchParams,
}: {
  searchParams?: Promise<{ period?: string; type?: string }>;
}) {
  const params = (await searchParams) ?? {};
  const requestedPeriod = params.period;
  const requestedType = params.type;
  const period: PeriodValue = requestedPeriod === "today" || requestedPeriod === "week" || requestedPeriod === "month" ? requestedPeriod : "month";
  const type: TypeValue = requestedType === "sale" || requestedType === "restock" || requestedType === "shrinkage" || requestedType === "all" ? requestedType : "all";

  let data: SalesLogResponse | null = null;

  try {
    data = await apiFetch<SalesLogResponse>(buildSalesLogPath(period, type));
  } catch {
    data = null;
  }

  return (
    <div className="min-h-screen bg-[#f4f6f8] md:flex">
      <Sidebar />

      <main className="min-w-0 flex-1">
        <header className="border-b border-slate-200 bg-white px-4 py-4 sm:px-6 lg:px-8">
          <div className="mx-auto flex max-w-7xl items-center justify-between gap-4">
            <div>
              <h1 className="text-xl font-semibold text-slate-950 sm:text-2xl">Transaksjonslogg</h1>
              <p className="mt-1 text-sm text-slate-500">Salg, svinn og påfyll for lageret</p>
            </div>

            <div className="flex items-center gap-2">
              <Link href="/demo-kasse" className="rounded-lg border border-slate-300 bg-white px-3 py-1.5 text-xs font-semibold text-slate-700 hover:bg-slate-50">
                Demo-Kasse
              </Link>
              <div className="rounded-full bg-slate-100 px-3 py-1.5 text-xs font-medium text-slate-600">Resepsjonslager</div>
            </div>
          </div>
        </header>

        <div className="mx-auto max-w-7xl space-y-6 px-4 py-6 sm:px-6 lg:px-8 lg:py-8">
          <section className="rounded-2xl border border-slate-200 bg-white p-4 shadow-sm sm:p-5">
            <div className="flex flex-wrap items-center gap-2">
              {periodOptions.map((option) => {
                const isActive = period === option.value;

                return (
                  <Link
                    key={option.value}
                    href={`/sales-log?period=${option.value}&type=${type}`}
                    className={isActive ? "rounded-lg bg-slate-900 px-3 py-1.5 text-xs font-medium text-white" : "rounded-lg bg-slate-100 px-3 py-1.5 text-xs font-medium text-slate-600 hover:bg-slate-200"}
                  >
                    {option.label}
                  </Link>
                );
              })}
            </div>
            <div className="mt-3 flex flex-wrap items-center gap-2">
              {typeOptions.map((option) => {
                const isActive = type === option.value;

                return (
                  <Link
                    key={option.value}
                    href={`/sales-log?period=${period}&type=${option.value}`}
                    className={isActive ? "rounded-lg bg-emerald-600 px-3 py-1.5 text-xs font-medium text-white" : "rounded-lg bg-slate-100 px-3 py-1.5 text-xs font-medium text-slate-600 hover:bg-slate-200"}
                  >
                    {option.label}
                  </Link>
                );
              })}
            </div>
          </section>

          {!data ? (
            <section className="rounded-2xl border border-rose-200 bg-rose-50 p-5 text-sm text-rose-800">
              <p className="font-medium">Klarte ikke å hente transaksjonslogg.</p>
              <p className="mt-1 text-rose-700">Sjekk at backend kjører og at API-et svarer på /api/sales.</p>
            </section>
          ) : (
            <>
              <section className="grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
                <article className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
                  <p className="text-sm font-medium text-slate-500">Solgte enheter</p>
                  <p className="mt-2 text-3xl font-semibold tracking-tight text-slate-950">{data.totalUnitsSold.toLocaleString("nb-NO")}</p>
                </article>
                <article className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
                  <p className="text-sm font-medium text-slate-500">Omsetning</p>
                  <p className="mt-2 text-3xl font-semibold tracking-tight text-slate-950">{formatNok(data.totalRevenue)}</p>
                </article>
                <article className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
                  <p className="text-sm font-medium text-slate-500">Svinnverdi</p>
                  <p className="mt-2 text-3xl font-semibold tracking-tight text-slate-950">{formatNok(data.totalShrinkageLoss)}</p>
                </article>
                <article className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
                  <p className="text-sm font-medium text-slate-500">Påfyllverdi</p>
                  <p className="mt-2 text-3xl font-semibold tracking-tight text-slate-950">{formatNok(data.totalRestockValue)}</p>
                </article>
              </section>

              <section className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm">
                <div className="overflow-x-auto">
                  <table className="min-w-full text-left text-sm">
                    <thead className="bg-slate-50 text-xs uppercase tracking-wide text-slate-500">
                      <tr>
                        <th className="px-5 py-3 font-medium">Tidspunkt</th>
                        <th className="px-5 py-3 font-medium">Type</th>
                        <th className="px-5 py-3 font-medium">Vare</th>
                        <th className="px-5 py-3 font-medium">Antall</th>
                        <th className="px-5 py-3 font-medium">Enhetspris</th>
                        <th className="px-5 py-3 font-medium">Totalt</th>
                        <th className="px-5 py-3 font-medium">Notat</th>
                      </tr>
                    </thead>
                    <tbody className="divide-y divide-slate-100">
                      {data.entries.map((entry) => (
                        <tr key={entry.id} className="hover:bg-slate-50/70">
                          <td className="px-5 py-4 text-slate-600">{formatDate(entry.createdAt)}</td>
                          <td className="px-5 py-4 text-slate-700">{entry.type}</td>
                          <td className="px-5 py-4 font-medium text-slate-900">{entry.itemName}</td>
                          <td className="px-5 py-4 text-slate-700">{entry.quantity}</td>
                          <td className="px-5 py-4 text-slate-700">{formatNok(entry.unitPrice)}</td>
                          <td className="px-5 py-4 text-slate-700">{formatNok(entry.total)}</td>
                          <td className="px-5 py-4 text-slate-700">{entry.note ?? "-"}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>

                {data.entries.length === 0 ? <p className="px-5 py-6 text-sm text-slate-500">Ingen transaksjoner for valgt filter.</p> : null}
              </section>
            </>
          )}
        </div>
      </main>
    </div>
  );
}
