"use client";

import { useState } from "react";
import Sidebar from "@/components/Sidebar";

type StockItem = {
  name: string;
  quantity: number;
  isTool?: boolean;
};

const initialItems: StockItem[] = [
  { name: "Batteri AA", quantity: 42 },
  { name: "Batteri AAA", quantity: 38 },
  { name: "Fjernkontroller", quantity: 6 },
  { name: "Ørepropper", quantity: 20 },
  { name: "Hammer", quantity: 0, isTool: true },
  { name: "Skrutrekker", quantity: 0, isTool: true },
  { name: "Vanlig verktøy", quantity: 4, isTool: true },
  { name: "Spikere", quantity: 300 },
  { name: "Lyspærer", quantity: 28 },
  { name: "Nøkkelutstyr", quantity: 14 },
  { name: "Bånd", quantity: 16 },
];

export default function MaintenanceLagerPage() {
  const [items, setItems] = useState(initialItems);

  function updateQuantity(name: string, delta: number) {
    setItems((current) =>
      current.map((item) =>
        item.name === name
          ? { ...item, quantity: Math.max(0, item.quantity + delta) }
          : item,
      ),
    );
  }

  return (
    <div className="min-h-screen bg-[#f4f6f8] md:flex">
      <Sidebar />

      <main className="min-w-0 flex-1">
        <header className="border-b border-slate-200 bg-white px-4 py-4 sm:px-6 lg:px-8">
          <div className="mx-auto max-w-5xl">
            <h1 className="text-xl font-semibold text-slate-950 sm:text-2xl">Maintenance lager</h1>
            <p className="mt-1 text-sm text-slate-500">Fokus på tilgjengelig utstyr</p>
          </div>
        </header>

        <div className="mx-auto max-w-5xl px-4 py-6 sm:px-6 lg:px-8">
          <section className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm">
            <table className="min-w-full text-left text-sm">
              <thead className="bg-slate-50 text-xs uppercase tracking-wide text-slate-500">
                <tr>
                  <th className="px-5 py-3 font-medium">Produkt</th>
                  <th className="px-5 py-3 font-medium">Antall</th>
                  <th className="px-5 py-3 font-medium">Juster</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-slate-100">
                {items.map((item) => {
                  const highlightMissingTool = item.isTool && item.quantity === 0;

                  return (
                    <tr key={item.name} className={highlightMissingTool ? "bg-rose-50" : ""}>
                      <td className={`px-5 py-4 font-medium ${highlightMissingTool ? "text-rose-700" : "text-slate-900"}`}>{item.name}</td>
                      <td className={`px-5 py-4 ${highlightMissingTool ? "font-semibold text-rose-700" : "text-slate-700"}`}>{item.quantity}</td>
                      <td className="px-5 py-4">
                        <div className="flex gap-2">
                          <button
                            type="button"
                            onClick={() => updateQuantity(item.name, -1)}
                            className="h-8 w-8 rounded-md border border-slate-300 text-slate-700 hover:bg-slate-100"
                          >
                            −
                          </button>
                          <button
                            type="button"
                            onClick={() => updateQuantity(item.name, 1)}
                            className="h-8 w-8 rounded-md border border-slate-300 text-slate-700 hover:bg-slate-100"
                          >
                            +
                          </button>
                        </div>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </section>
        </div>
      </main>
    </div>
  );
}
