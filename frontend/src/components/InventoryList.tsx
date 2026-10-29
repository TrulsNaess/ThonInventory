"use client";

import { useState } from "react";
import ProductDetailsPanel from "@/components/ProductDetailsPanel";
import type { InventoryCategory, InventoryItem } from "@/types/inventory";

type Props = {
  categories: InventoryCategory[];
};

type SelectedProduct = {
  item: InventoryItem;
  categoryName: string;
};

function formatDate(value: string | null) {
  if (!value) return "-";

  return new Intl.DateTimeFormat("nb-NO", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
  }).format(new Date(value));
}

function formatNok(value: number) {
  if (value === 0) return "Gratis";

  return new Intl.NumberFormat("nb-NO", {
    style: "currency",
    currency: "NOK",
    maximumFractionDigits: 0,
  }).format(value);
}

function renderStatus(item: InventoryItem) {
  if (item.isOutOfStock) {
    return (
      <span className="whitespace-nowrap rounded-full bg-rose-50 px-2.5 py-1 text-xs font-medium text-rose-700">
        Utsolgt
      </span>
    );
  }

  if (item.isDiscrepancy) {
    return (
      <span className="whitespace-nowrap rounded-full bg-violet-50 px-2.5 py-1 text-xs font-medium text-violet-700">
        Avvik
      </span>
    );
  }

  return (
    <span className="whitespace-nowrap rounded-full bg-emerald-50 px-2.5 py-1 text-xs font-medium text-emerald-700">
      På lager
    </span>
  );
}

function getProductImage(item: InventoryItem) {
  const imageMap: Record<string, string> = {
    "Coca-Cola": "/products/CocaColaLogo.jpg",
    "Coca-Cola Zero": "/products/ColaZeroLogo.jpg",
    Fanta: "/products/FantaLogo.jpg",
    Sprite: "/products/SpriteLogo.jpg",
    Eplemost: "/products/EplemostLogo.jpg",
    Mineralvann: "/products/VannLogo.jpg",
    "Mineralvann med kullsyre": "/products/FarrisLogo.jpg",
    "Red Bull": "/products/RedbullLogo.jpg",
    Monster: "/products/MonsterLogo.png",

    Stratos: "/products/StratosLogo.jpg",
    Melkesjokolade: "/products/MelkesjokoladeLogo.jpg",
    Sørlandschips: "/products/SorlandschipsLogo.jpg",
    "Sørlandschips (små poser)": "/products/SorlandschipsLogo.jpg",
    Chillinøtter: "/products/ChillinotterLogo.png",
    Peanøtter: "/products/PeanotterLogo.jpg",
    "Polly Nøtter": "/products/PollyNotterLogo.jpg",
    "Extra tyggis": "/products/ExtraTyggisLogo.jpg",
    "Små sulten": "/products/SmaSultenLogo.jpg",
  };

  return imageMap[item.name] ?? "";
}

function ProductInfo({ item }: { item: InventoryItem }) {
  const imageSrc = getProductImage(item);

  return (
    <div className="flex min-w-0 items-center gap-3">
      {imageSrc ? (
        <div className="flex h-12 w-12 shrink-0 items-center justify-center overflow-hidden rounded-xl border border-slate-200 bg-white p-1">
          <img
            src={imageSrc}
            alt={item.name}
            className="h-full w-full object-contain"
          />
        </div>
      ) : (
        <div className="flex h-12 w-12 shrink-0 items-center justify-center rounded-xl border border-slate-200 bg-slate-50 text-xs font-medium text-slate-400">
          Ingen
        </div>
      )}

      <span className="min-w-0 truncate font-medium text-slate-900">
        {item.name}
      </span>
    </div>
  );
}

export default function InventoryList({ categories }: Props) {
  const [selectedProduct, setSelectedProduct] =
    useState<SelectedProduct | null>(null);

  if (
    categories.length === 0 ||
    categories.every((category) => category.items.length === 0)
  ) {
    return (
      <div className="rounded-2xl border border-dashed border-slate-300 bg-white p-10 text-center text-sm text-slate-500">
        Ingen varer passer med filtreringen.
      </div>
    );
  }

  return (
    <>
      <div className="space-y-5">
        {categories.map((category) =>
          category.items.length > 0 ? (
            <section
              key={category.id}
              className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm"
            >
              <div className="flex items-center justify-between border-b border-slate-100 px-5 py-4">
                <div>
                  <h2 className="font-semibold text-slate-900">
                    {category.name}
                  </h2>

                  <p className="mt-0.5 text-xs text-slate-500">
                    {category.items.length} varer vist
                  </p>
                </div>

                <p className="hidden text-xs text-slate-400 md:block">
                  Klikk på en vare for detaljer
                </p>
              </div>

              <div className="hidden w-full overflow-hidden md:block">
                <table className="w-full table-fixed text-left text-sm">
                  <thead className="bg-slate-50 text-xs uppercase tracking-wide text-slate-500">
                    <tr>
                      <th className="w-[30%] px-5 py-3 font-medium">
                        Produkt
                      </th>

                      <th className="w-[9%] px-3 py-3 font-medium">
                        Antall
                      </th>

                      <th className="w-[10%] px-3 py-3 font-medium">
                        Pris
                      </th>

                      <th className="w-[11%] px-3 py-3 font-medium">
                        Verdi
                      </th>

                      <th className="w-[13%] px-3 py-3 font-medium">
                        Status
                      </th>

                      <th className="hidden w-[12%] px-3 py-3 font-medium xl:table-cell">
                        Bestilling
                      </th>

                      <th className="hidden w-[12%] px-3 py-3 font-medium xl:table-cell">
                        Levering
                      </th>

                      <th className="hidden px-3 py-3 font-medium 2xl:table-cell">
                        Mangler siden
                      </th>

                      <th className="hidden px-3 py-3 font-medium 2xl:table-cell">
                        Notat
                      </th>

                      <th className="w-8 px-2 py-3" />
                    </tr>
                  </thead>

                  <tbody className="divide-y divide-slate-100">
                    {category.items.map((item) => (
                      <tr
                        key={item.id}
                        tabIndex={0}
                        onClick={() =>
                          setSelectedProduct({
                            item,
                            categoryName: category.name,
                          })
                        }
                        onKeyDown={(event) => {
                          if (
                            event.key === "Enter" ||
                            event.key === " "
                          ) {
                            event.preventDefault();

                            setSelectedProduct({
                              item,
                              categoryName: category.name,
                            });
                          }
                        }}
                        className="group cursor-pointer transition-colors hover:bg-slate-50 focus:bg-slate-50 focus:outline-none"
                      >
                        <td className="min-w-0 px-5 py-3">
                          <ProductInfo item={item} />
                        </td>

                        <td className="whitespace-nowrap px-3 py-4 text-slate-700">
                          {item.quantity}
                        </td>

                        <td className="whitespace-nowrap px-3 py-4 text-slate-700">
                          {formatNok(item.price)}
                        </td>

                        <td className="whitespace-nowrap px-3 py-4 text-slate-700">
                          {item.price === 0
                            ? "-"
                            : formatNok(item.stockValue)}
                        </td>

                        <td className="px-3 py-4">
                          {renderStatus(item)}
                        </td>

                        <td className="hidden px-3 py-4 xl:table-cell">
                          {item.isOrdered ? (
                            <span className="whitespace-nowrap rounded-full bg-amber-50 px-2.5 py-1 text-xs font-medium text-amber-800">
                              Bestilt
                            </span>
                          ) : (
                            <span className="text-slate-400">-</span>
                          )}
                        </td>

                        <td className="hidden whitespace-nowrap px-3 py-4 text-slate-600 xl:table-cell">
                          {formatDate(item.expectedDeliveryDate)}
                        </td>

                        <td className="hidden whitespace-nowrap px-3 py-4 text-slate-600 2xl:table-cell">
                          {formatDate(item.missingSinceDate)}
                        </td>

                        <td className="hidden min-w-0 px-3 py-4 text-slate-600 2xl:table-cell">
                          <div className="truncate">
                            {item.notes || "-"}
                          </div>
                        </td>

                        <td className="px-2 py-4 text-right text-lg text-slate-300 transition group-hover:translate-x-0.5 group-hover:text-slate-600">
                          ›
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>

              <div className="divide-y divide-slate-100 md:hidden">
                {category.items.map((item) => (
                  <button
                    type="button"
                    key={item.id}
                    onClick={() =>
                      setSelectedProduct({
                        item,
                        categoryName: category.name,
                      })
                    }
                    className="block w-full p-4 text-left transition hover:bg-slate-50"
                  >
                    <div className="flex items-start justify-between gap-3">
                      <ProductInfo item={item} />

                      <div className="flex shrink-0 flex-wrap justify-end gap-1.5">
                        {renderStatus(item)}

                        {item.isOrdered ? (
                          <span className="rounded-full bg-amber-50 px-2 py-1 text-[11px] font-medium text-amber-800">
                            Bestilt
                          </span>
                        ) : null}
                      </div>
                    </div>

                    <div className="mt-3 grid grid-cols-2 gap-2 text-sm text-slate-600">
                      <p>Antall: {item.quantity}</p>

                      <p>
                        Pris:{" "}
                        {item.price === 0
                          ? "Gratis for medlemmer"
                          : formatNok(item.price)}
                      </p>

                      <p>
                        Verdi:{" "}
                        {item.price === 0
                          ? "-"
                          : formatNok(item.stockValue)}
                      </p>

                      <p>
                        Levering:{" "}
                        {formatDate(item.expectedDeliveryDate)}
                      </p>
                    </div>

                    <div className="mt-3 flex items-center justify-between border-t border-slate-100 pt-3 text-xs text-slate-400">
                      <span>Åpne produktdetaljer</span>
                      <span className="text-base">›</span>
                    </div>
                  </button>
                ))}
              </div>
            </section>
          ) : null,
        )}
      </div>

      {selectedProduct ? (
        <ProductDetailsPanel
          item={selectedProduct.item}
          categoryName={selectedProduct.categoryName}
          imageSrc={getProductImage(selectedProduct.item)}
          onClose={() => setSelectedProduct(null)}
        />
      ) : null}
    </>
  );
}