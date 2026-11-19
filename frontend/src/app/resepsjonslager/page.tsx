"use client";

import { useEffect, useState } from "react";
import Sidebar from "@/components/Sidebar";
import { apiFetch } from "@/lib/api";

type StockItem = {
    id: number;
    name: string;
    quantity: number;
};

export default function ResepsjonslagerPage() {
    const [items, setItems] = useState<StockItem[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [updatingItemId, setUpdatingItemId] = useState<number | null>(null);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        async function loadItems() {
            try {
                setError(null);

                const response = await apiFetch<StockItem[]>(
                    "/api/location-inventory/resepsjon",
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
                `/api/location-inventory/resepsjon/${itemId}/adjust`,
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
                            Resepsjonslager
                        </h1>

                        <p className="mt-1 text-sm text-slate-500">
                            Liste med produkt og antall
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
                        <table className="min-w-full text-left text-sm">
                            <thead className="bg-slate-50 text-xs uppercase tracking-wide text-slate-500">
                                <tr>
                                    <th className="px-5 py-3 font-medium">Produkt</th>
                                    <th className="px-5 py-3 font-medium">Antall</th>
                                    <th className="px-5 py-3 font-medium">Juster</th>
                                </tr>
                            </thead>

                            <tbody className="divide-y divide-slate-100">
                                {isLoading ? (
                                    <tr>
                                        <td
                                            colSpan={3}
                                            className="px-5 py-6 text-sm text-slate-500"
                                        >
                                            Henter lager...
                                        </td>
                                    </tr>
                                ) : (
                                    items.map((item) => {
                                        const isUpdating = updatingItemId === item.id;

                                        return (
                                            <tr key={item.id}>
                                                <td className="px-5 py-4 font-medium text-slate-900">
                                                    {item.name}
                                                </td>

                                                <td className="px-5 py-4 text-slate-700">
                                                    {item.quantity}
                                                </td>

                                                <td className="px-5 py-4">
                                                    <div className="flex gap-2">
                                                        <button
                                                            type="button"
                                                            disabled={item.quantity <= 0 || isUpdating}
                                                            onClick={() =>
                                                                void updateQuantity(item.id, -1)
                                                            }
                                                            className="h-8 w-8 rounded-md border border-slate-300 text-slate-700 hover:bg-slate-100 disabled:cursor-not-allowed disabled:opacity-40"
                                                        >
                                                            −
                                                        </button>

                                                        <button
                                                            type="button"
                                                            disabled={isUpdating}
                                                            onClick={() =>
                                                                void updateQuantity(item.id, 1)
                                                            }
                                                            className="h-8 w-8 rounded-md border border-slate-300 text-slate-700 hover:bg-slate-100 disabled:cursor-not-allowed disabled:opacity-40"
                                                        >
                                                            +
                                                        </button>
                                                    </div>
                                                </td>
                                            </tr>
                                        );
                                    })
                                )}
                            </tbody>
                        </table>
                    </section>
                </div>
            </main>
        </div>
    );
}