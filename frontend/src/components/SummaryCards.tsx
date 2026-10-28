type Props = {
  total: number;
  totalQuantity: number;
  missing: number;
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

export default function SummaryCards({ total, totalQuantity, missing, ordered, totalValue }: Props) {
  const cards = [
    { label: "Varer totalt", value: total.toLocaleString("nb-NO"), detail: "Registrert i lageret" },
    { label: "Beholdning", value: totalQuantity.toLocaleString("nb-NO"), detail: "Antall enheter på lager" },
    { label: "Lagerverdi", value: formatNok(totalValue), detail: "Total verdi av nåværende beholdning" },
    { label: "Mangler", value: missing.toLocaleString("nb-NO"), detail: "Varer som må følges opp" },
    { label: "Bestilt", value: ordered.toLocaleString("nb-NO"), detail: "Varer med aktiv bestilling" },
  ];

  return (
    <section className="grid gap-4 sm:grid-cols-2 xl:grid-cols-5">
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
