type Props = {
  total: number;
  missing: number;
  ordered: number;
};

export default function SummaryCards({ total, missing, ordered }: Props) {
  const cards = [
    { label: "Varer totalt", value: total, detail: "Registrert i lageret" },
    { label: "Mangler", value: missing, detail: "Varer som må følges opp" },
    { label: "Bestilt", value: ordered, detail: "Registrert som bestilt" },
  ];

  return (
    <section className="grid gap-4 sm:grid-cols-3">
      {cards.map((card) => (
        <article key={card.label} className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <p className="text-sm font-medium text-slate-500">{card.label}</p>
          <p className="mt-2 text-3xl font-semibold tracking-tight text-slate-950">{card.value}</p>
          <p className="mt-1 text-xs text-slate-400">{card.detail}</p>
        </article>
      ))}
    </section>
  );
}
