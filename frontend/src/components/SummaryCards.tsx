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

export default function SummaryCards({
    total,
    totalQuantity,
    outOfStock,
    discrepancy,
    ordered,
    totalValue,
}: Props) {
    const cards = [
        {
            label: "Varer totalt",
            value: total.toLocaleString("nb-NO"),
            detail: "Registrert i lageret",
        },
        {
            label: "Beholdning",
            value: totalQuantity.toLocaleString("nb-NO"),
            detail: "Antall enheter på lager",
        },
        {
            label: "Lagerverdi",
            value: formatNok(totalValue),
            detail: "Total verdi på lager",
        },
        {
            label: "Utsolgt",
            value: outOfStock.toLocaleString("nb-NO"),
            detail: "Varer med 0 på lager",
        },
        {
            label: "Avvik",
            value: discrepancy.toLocaleString("nb-NO"),
            detail: "Registrerte lageravvik",
        },
        {
            label: "Bestilt",
            value: ordered.toLocaleString("nb-NO"),
            detail: "Varer med aktiv bestilling",
        },
    ];

    return (
        <section className="rounded-2xl border border-slate-300 bg-slate-100/60 p-4 shadow-sm">
            <div className="grid gap-3 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-6">
                {cards.map((card) => (
                    <article
                        key={card.label}
                        className="
              rounded-xl
              border border-slate-200
              bg-white
              px-5 py-4
              shadow-sm
              transition-all duration-200
              hover:-translate-y-0.5
              hover:border-slate-300
              hover:shadow-md
            "
                    >
                        <p className="text-sm font-medium text-slate-500">
                            {card.label}
                        </p>

                        <p className="mt-2 whitespace-nowrap text-2xl font-bold tracking-tight text-slate-950">
                            {card.value}
                        </p>

                        <p className="mt-2 text-xs leading-5 text-slate-400">
                            {card.detail}
                        </p>
                    </article>
                ))}
            </div>
        </section>
    );
}