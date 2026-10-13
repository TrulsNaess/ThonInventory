import type { InventoryCategory } from "@/types/inventory";

type Props = {
  categories: InventoryCategory[];
};

function formatDate(value: string | null) {
  if (!value) return "-";

  return new Intl.DateTimeFormat("nb-NO", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
  }).format(new Date(value));
}

export default function InventoryList({ categories }: Props) {
  if (categories.length === 0 || categories.every((category) => category.items.length === 0)) {
    return (
      <div className="rounded-2xl border border-dashed border-slate-300 bg-white p-10 text-center text-sm text-slate-500">
        Ingen varer passer med filtreringen.
      </div>
    );
  }

  return (
    <div className="space-y-5">
      {categories.map((category) =>
        category.items.length > 0 ? (
          <section key={category.id} className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm">
            <div className="flex items-center justify-between border-b border-slate-100 px-5 py-4">
              <div>
                <h2 className="font-semibold text-slate-900">{category.name}</h2>
                <p className="mt-0.5 text-xs text-slate-500">{category.items.length} varer vist</p>
              </div>
            </div>

            <div className="hidden overflow-x-auto md:block">
              <table className="min-w-full text-left text-sm">
                <thead className="bg-slate-50 text-xs uppercase tracking-wide text-slate-500">
                  <tr>
                    <th className="px-5 py-3 font-medium">Produkt</th>
                    <th className="px-5 py-3 font-medium">Status</th>
                    <th className="px-5 py-3 font-medium">Mangler siden</th>
                    <th className="px-5 py-3 font-medium">Notat</th>
                    <th className="px-5 py-3 font-medium">Bestilling</th>
                  </tr>
                </thead>
                <tbody className="divide-y divide-slate-100">
                  {category.items.map((item) => (
                    <tr key={item.id} className="hover:bg-slate-50/70">
                      <td className="px-5 py-4 font-medium text-slate-900">{item.name}</td>
                      <td className="px-5 py-4">
                        {item.isMissing ? (
                          <span className="rounded-full bg-rose-50 px-2.5 py-1 text-xs font-medium text-rose-700">Mangler</span>
                        ) : (
                          <span className="rounded-full bg-emerald-50 px-2.5 py-1 text-xs font-medium text-emerald-700">På lager</span>
                        )}
                      </td>
                      <td className="px-5 py-4 text-slate-600">{formatDate(item.missingSinceDate)}</td>
                      <td className="max-w-xs px-5 py-4 text-slate-600">{item.notes || "-"}</td>
                      <td className="px-5 py-4">
                        {item.isOrdered ? (
                          <div>
                            <span className="rounded-full bg-amber-50 px-2.5 py-1 text-xs font-medium text-amber-800">Bestilt</span>
                            <p className="mt-1.5 text-xs text-slate-400">{formatDate(item.orderedAtDate)}</p>
                          </div>
                        ) : (
                          <span className="text-slate-400">-</span>
                        )}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>

            <div className="divide-y divide-slate-100 md:hidden">
              {category.items.map((item) => (
                <article key={item.id} className="p-4">
                  <div className="flex items-start justify-between gap-3">
                    <p className="font-medium text-slate-900">{item.name}</p>
                    <div className="flex shrink-0 flex-wrap justify-end gap-1.5">
                      {item.isMissing ? (
                        <span className="rounded-full bg-rose-50 px-2 py-1 text-[11px] font-medium text-rose-700">Mangler</span>
                      ) : (
                        <span className="rounded-full bg-emerald-50 px-2 py-1 text-[11px] font-medium text-emerald-700">På lager</span>
                      )}
                      {item.isOrdered ? (
                        <span className="rounded-full bg-amber-50 px-2 py-1 text-[11px] font-medium text-amber-800">Bestilt</span>
                      ) : null}
                    </div>
                  </div>
                  {item.notes ? <p className="mt-2 text-sm text-slate-600">{item.notes}</p> : null}
                  {(item.missingSinceDate || item.orderedAtDate) && (
                    <div className="mt-3 flex flex-wrap gap-x-4 gap-y-1 text-xs text-slate-400">
                      {item.missingSinceDate ? <span>Mangler siden {formatDate(item.missingSinceDate)}</span> : null}
                      {item.orderedAtDate ? <span>Bestilt {formatDate(item.orderedAtDate)}</span> : null}
                    </div>
                  )}
                </article>
              ))}
            </div>
          </section>
        ) : null,
      )}
    </div>
  );
}
