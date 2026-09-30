import { apiFetch } from "@/lib/api";
import type { InventoryResponse } from "@/types/inventory";

export default async function Home() {
  let data: InventoryResponse | null = null;
  let loadError = false;

  try {
    data = await apiFetch<InventoryResponse>("/api/inventory");
  } catch {
    loadError = true;
  }

  const allItems = data?.categories.flatMap((category) => category.items) ?? [];
  const missingCount = allItems.filter((item) => item.isMissing).length;
  const orderedCount = allItems.filter((item) => item.isOrdered).length;

  return (
    <main className="min-h-screen bg-slate-50 p-8">
      <div className="mx-auto max-w-6xl space-y-8">
        <header>
          <p className="mb-2 text-sm font-medium text-slate-500">Thon Hotel Kristiansand</p>
          <h1 className="text-4xl font-semibold tracking-tight text-slate-900">Inventory</h1>
          <p className="mt-3 max-w-3xl text-slate-600">
            Enkel oversikt for beholdning, registrering av salg og svinn basert på data fra backend.
          </p>
        </header>

        {loadError ? (
          <section className="rounded-lg border border-rose-200 bg-rose-50 p-4 text-rose-700">
            Klarte ikke å hente inventory-data fra backend. Sjekk at API-et kjører.
          </section>
        ) : (
          <>
            <section className="grid grid-cols-1 gap-4 sm:grid-cols-3">
              <article className="rounded-lg border border-slate-200 bg-white p-4">
                <p className="text-sm text-slate-500">Varer</p>
                <p className="mt-1 text-2xl font-semibold text-slate-900">{allItems.length}</p>
              </article>
              <article className="rounded-lg border border-slate-200 bg-white p-4">
                <p className="text-sm text-slate-500">Markert som svinn/mangler</p>
                <p className="mt-1 text-2xl font-semibold text-slate-900">{missingCount}</p>
              </article>
              <article className="rounded-lg border border-slate-200 bg-white p-4">
                <p className="text-sm text-slate-500">På bestilling</p>
                <p className="mt-1 text-2xl font-semibold text-slate-900">{orderedCount}</p>
              </article>
            </section>

            {data?.categories.map((category) => (
              <section key={category.id} className="rounded-xl border border-slate-200 bg-white p-5 shadow-sm">
                <h2 className="mb-4 text-xl font-semibold text-slate-900">{category.name}</h2>
                <div className="overflow-x-auto">
                  <table className="min-w-full text-left text-sm">
                    <thead className="border-b border-slate-200 text-slate-500">
                      <tr>
                        <th className="px-2 py-2 font-medium">Vare</th>
                        <th className="px-2 py-2 font-medium">Pris</th>
                        <th className="px-2 py-2 font-medium">Status</th>
                        <th className="px-2 py-2 font-medium">Beholdning</th>
                        <th className="px-2 py-2 font-medium">Salg</th>
                        <th className="px-2 py-2 font-medium">Svinn</th>
                      </tr>
                    </thead>
                    <tbody>
                      {category.items.map((item) => (
                        <tr key={item.id} className="border-b border-slate-100 align-top">
                          <td className="px-2 py-3">
                            <p className="font-medium text-slate-900">{item.name}</p>
                            {item.notes ? <p className="text-xs text-slate-500">{item.notes}</p> : null}
                          </td>
                          <td className="px-2 py-3 text-slate-700">{item.price.toFixed(2)} kr</td>
                          <td className="px-2 py-3">
                            <div className="flex flex-wrap gap-2 text-xs">
                              {item.isMissing ? (
                                <span className="rounded-full bg-rose-100 px-2 py-1 text-rose-700">Svinn/mangler</span>
                              ) : (
                                <span className="rounded-full bg-emerald-100 px-2 py-1 text-emerald-700">Tilgjengelig</span>
                              )}
                              {item.isOrdered ? (
                                <span className="rounded-full bg-blue-100 px-2 py-1 text-blue-700">Bestilt</span>
                              ) : null}
                            </div>
                          </td>
                          <td className="px-2 py-3">
                            <input
                              type="number"
                              min={0}
                              defaultValue={0}
                              className="w-24 rounded-md border border-slate-300 px-2 py-1"
                            />
                          </td>
                          <td className="px-2 py-3">
                            <input
                              type="number"
                              min={0}
                              defaultValue={0}
                              className="w-24 rounded-md border border-slate-300 px-2 py-1"
                            />
                          </td>
                          <td className="px-2 py-3">
                            <input
                              type="number"
                              min={0}
                              defaultValue={0}
                              className="w-24 rounded-md border border-slate-300 px-2 py-1"
                            />
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </section>
            ))}
          </>
        )}
      </div>
    </main>
  );
}
