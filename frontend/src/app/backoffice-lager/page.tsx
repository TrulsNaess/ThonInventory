"use client";

import { useState } from "react";
import Sidebar from "@/components/Sidebar";

type StockItem = {
  name: string;
  quantity: number;
};

const initialItems: StockItem[] = [
  { name: "Håndkler (store)", quantity: 45 },
  { name: "Håndkler (små)", quantity: 70 },
  { name: "Ørepropper", quantity: 120 },
  { name: "Såpe", quantity: 90 },
  { name: "Puter", quantity: 16 },
  { name: "Dopapir", quantity: 64 },
  { name: "Klesruller", quantity: 18 },
  { name: "Tørkepapir", quantity: 25 },
  { name: "Flipflopper / sandaler", quantity: 22 },
  { name: "Tannkremer", quantity: 50 },
  { name: "Antibac", quantity: 14 },
  { name: "Blekk til printer", quantity: 9 },
  { name: "Ansiktsmasker", quantity: 40 },
  { name: "Ekstra ovn", quantity: 2 },
  { name: "Fluesmekker", quantity: 11 },
];

export default function BackofficeLagerPage() {
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
            <h1 className="text-xl font-semibold text-slate-950 sm:text-2xl">Backoffice lager</h1>
            <p className="mt-1 text-sm text-slate-500">Liste med produkt og antall</p>
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
                {items.map((item) => (
                  <tr key={item.name}>
                    <td className="px-5 py-4 font-medium text-slate-900">{item.name}</td>
                    <td className="px-5 py-4 text-slate-700">{item.quantity}</td>
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
                ))}
              </tbody>
            </table>
          </section>
        </div>
      </main>
    </div>
  );
}
