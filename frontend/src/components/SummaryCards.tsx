type Props = {
  total: number;
  totalQuantity: number;
  outOfStock: number;
  discrepancy: number;
  ordered: number;
  totalValue: number;
};

function formatNok(value: number) {
  return new Intl.NumberFormat("nb-NO", {
    style: "currency",
    currency: "NOK",
    maximumFractionDigits: 0,
  }).format(value);
}

export default function SummaryCards({ total, totalQuantity, outOfStock, discrepancy, ordered, totalValue }: Props) {
  const cards = [
    { label: "Varer totalt", value: total.toLocaleString("nb-NO"), detail: "Registrert i lageret" },
    { label: "Beholdning", value: totalQuantity.toLocaleString("nb-NO"), detail: "Antall enheter på lager" },
    { label: "Lagerverdi", value: formatNok(totalValue), detail: "Total verdi av nåværende beholdning" },
    { label: "Utsolgt", value: outOfStock.toLocaleString("nb-NO"), detail: "Varer med 0 på lager" },
    { label: "Avvik", value: discrepancy.toLocaleString("nb-NO"), detail: "Manuelle mangler med positivt lager" },
    { label: "Bestilt", value: ordered.toLocaleString("nb-NO"), detail: "Varer med aktiv bestilling" },
  ];

  return (
    <section className="grid gap-4 sm:grid-cols-2 xl:grid-cols-6">
      {cards.map((card) => (
        <article key={card.label} className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <p className="text-sm font-medium text-slate-500">{card.label}</p>
          <p className="mt-2 text-2xl font-semibold tracking-tight text-slate-950 xl:text-3xl">{card.value}</p>
          <p className="mt-1 text-xs text-slate-400">{card.detail}</p>
        </article>
      ))}
    </section>
  );
}
