"use client";

import { useEffect, useMemo, useState } from "react";
import Link from "next/link";
import Sidebar from "@/components/Sidebar";
import { apiFetch, apiPost } from "@/lib/api";
import type { DemoCheckoutResponse, InventoryItem, InventoryResponse } from "@/types/inventory";

type CartLine = {
  itemId: number;
  itemName: string;
  quantity: number;
  unitPrice: number;
};

function formatNok(value: number) {
  return new Intl.NumberFormat("nb-NO", {
    style: "currency",
    currency: "NOK",
    maximumFractionDigits: 0,
  }).format(value);
}

export default function DemoKassePage() {
  const [data, setData] = useState<InventoryResponse | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [itemId, setItemId] = useState(0);
  const [quantity, setQuantity] = useState(1);
  const [cart, setCart] = useState<CartLine[]>([]);
  const [isSelling, setIsSelling] = useState(false);

  useEffect(() => {
    void (async () => {
      try {
        const response = await apiFetch<InventoryResponse>("/api/inventory");
        setData(response);
      } finally {
        setIsLoading(false);
      }
    })();
  }, []);

  const items = useMemo(() => data?.categories.flatMap((category) => category.items) ?? [], [data]);

  function addToCart() {
    if (itemId <= 0 || quantity <= 0) {
      return;
    }

    const selected = items.find((item) => item.id === itemId);

    if (!selected) {
      return;
    }

    setCart((current) => {
      const existing = current.find((line) => line.itemId === itemId);
      if (existing) {
        return current.map((line) =>
          line.itemId === itemId
            ? { ...line, quantity: line.quantity + quantity }
            : line,
        );
      }

      return [...current, { itemId: selected.id, itemName: selected.name, quantity, unitPrice: selected.price }];
    });
  }

  async function sellCart() {
    if (cart.length === 0) {
      return;
    }

    setIsSelling(true);

    try {
      await apiPost<DemoCheckoutResponse, { lines: { itemId: number; quantity: number; unitPrice: number }[]; note: string }>("/api/sales/demo-checkout", {
        lines: cart.map((line) => ({ itemId: line.itemId, quantity: line.quantity, unitPrice: line.unitPrice })),
        note: "Demo-Kasse",
      });

      window.location.href = "/sales-log?period=today&type=sale";
    } finally {
      setIsSelling(false);
    }
  }

  const cartTotal = cart.reduce((sum, line) => sum + line.quantity * line.unitPrice, 0);

  return (
    <div className="min-h-screen bg-[#f4f6f8] md:flex">
      <Sidebar />

      <main className="min-w-0 flex-1">
        <header className="border-b border-slate-200 bg-white px-4 py-4 sm:px-6 lg:px-8">
          <div className="mx-auto flex max-w-7xl items-center justify-between gap-4">
            <div>
              <h1 className="text-xl font-semibold text-slate-950 sm:text-2xl">Demo-Kasse</h1>
              <p className="mt-1 text-sm text-slate-500">Test enkel utsjekk med produkt + antall</p>
            </div>

            <Link href="/" className="rounded-lg border border-slate-300 bg-white px-3 py-1.5 text-xs font-semibold text-slate-700 hover:bg-slate-50">
              Til lager
            </Link>
          </div>
        </header>

        <div className="mx-auto max-w-4xl space-y-4 px-4 py-6 sm:px-6 lg:px-8">
          {isLoading ? (
            <section className="rounded-2xl border border-slate-200 bg-white p-5 text-sm text-slate-600">Laster varer...</section>
          ) : (
            <>
              <section className="rounded-2xl border border-slate-200 bg-white p-4 shadow-sm">
                <div className="flex gap-2">
                  <select className="flex-1 rounded-lg border border-slate-300 px-3 py-2 text-sm" value={itemId} onChange={(event) => setItemId(Number(event.target.value))}>
                    <option value={0}>Velg produkt</option>
                    {items.map((item: InventoryItem) => (
                      <option key={item.id} value={item.id}>
                        {item.name} ({item.quantity} på lager)
                      </option>
                    ))}
                  </select>
                  <input className="w-24 rounded-lg border border-slate-300 px-3 py-2 text-sm" min={1} type="number" value={quantity} onChange={(event) => setQuantity(Number(event.target.value))} />
                  <button type="button" onClick={addToCart} className="rounded-lg bg-slate-900 px-3 py-2 text-sm font-medium text-white">
                    Legg til
                  </button>
                </div>
              </section>

              <section className="rounded-2xl border border-slate-200 bg-white p-4 shadow-sm">
                <h2 className="font-semibold text-slate-900">Handlekurv</h2>
                {cart.length === 0 ? (
                  <p className="mt-2 text-sm text-slate-500">Ingen varer lagt til ennå.</p>
                ) : (
                  <ul className="mt-3 space-y-2">
                    {cart.map((line) => (
                      <li key={line.itemId} className="flex items-center justify-between rounded-lg border border-slate-200 px-3 py-2">
                        <div className="text-sm text-slate-700">
                          {line.itemName} x {line.quantity}
                        </div>
                        <div className="flex items-center gap-3">
                          <span className="text-sm font-medium text-slate-900">{formatNok(line.quantity * line.unitPrice)}</span>
                          <button
                            type="button"
                            onClick={() => setCart((current) => current.filter((entry) => entry.itemId !== line.itemId))}
                            className="rounded-md border border-slate-300 px-2 py-1 text-xs text-slate-600 hover:bg-slate-100"
                          >
                            ✕
                          </button>
                        </div>
                      </li>
                    ))}
                  </ul>
                )}

                <div className="mt-4 flex items-center justify-between border-t border-slate-200 pt-3">
                  <span className="text-sm text-slate-600">Totalt</span>
                  <span className="text-lg font-semibold text-slate-950">{formatNok(cartTotal)}</span>
                </div>

                <button
                  type="button"
                  disabled={cart.length === 0 || isSelling}
                  onClick={() => void sellCart()}
                  className="mt-4 w-full rounded-lg bg-emerald-600 px-4 py-2 text-sm font-semibold text-white disabled:cursor-not-allowed disabled:bg-emerald-300"
                >
                  {isSelling ? "Selger..." : "Selg"}
                </button>
              </section>
            </>
          )}
        </div>
      </main>
    </div>
  );
}
