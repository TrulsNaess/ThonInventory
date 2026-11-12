type Props = {
    name: string;
    count: number;
    missing: number;
    active: boolean;
    imageSrc: string;
    onClick: () => void;
};

const categoryStyles: Record<
    string,
    {
        card: string;
        active: string;
    }
> = {
    BRUS: {
        card: "border-blue-200 bg-gradient-to-br from-blue-50 via-white to-blue-50/40 hover:border-blue-400 hover:shadow-blue-100",
        active: "border-blue-400 ring-2 ring-blue-200 shadow-lg shadow-blue-100",
    },
    SNACKS: {
        card: "border-emerald-200 bg-gradient-to-br from-emerald-50 via-white to-emerald-50/40 hover:border-emerald-400 hover:shadow-emerald-100",
        active: "border-emerald-400 ring-2 ring-emerald-200 shadow-lg shadow-emerald-100",
    },
    ALKOHOL: {
        card: "border-violet-200 bg-gradient-to-br from-violet-50 via-white to-violet-50/40 hover:border-violet-400 hover:shadow-violet-100",
        active: "border-violet-400 ring-2 ring-violet-200 shadow-lg shadow-violet-100",
    },
    KIOSK: {
        card: "border-orange-200 bg-gradient-to-br from-orange-50 via-white to-orange-50/40 hover:border-orange-400 hover:shadow-orange-100",
        active: "border-orange-400 ring-2 ring-orange-200 shadow-lg shadow-orange-100",
    },
};

export default function CategoryCard({
    name,
    count,
    missing,
    active,
    imageSrc,
    onClick,
}: Props) {
    const styles = categoryStyles[name] ?? categoryStyles.BRUS;

    return (
        <button
            type="button"
            onClick={onClick}
            aria-pressed={active}
            className={`
                group relative overflow-hidden rounded-2xl border p-5 text-left
                transition-all duration-200 ease-out
                hover:-translate-y-1 hover:shadow-xl
                active:scale-[0.98]
                focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-slate-400
                ${styles.card}
                ${active ? styles.active : ""}
            `}
        >
            <div className="pointer-events-none absolute -right-8 -top-8 h-24 w-24 rounded-full bg-white/70 opacity-0 blur-2xl transition-opacity duration-300 group-hover:opacity-100" />

            <div className="relative flex items-start justify-between gap-4">
                <div className="flex min-w-0 items-center gap-3">
                    <div className="h-14 w-14 shrink-0 overflow-hidden rounded-xl border border-white/80 bg-white shadow-sm transition-transform duration-200 group-hover:scale-105">
                        <img
                            src={imageSrc}
                            alt={name}
                            className="h-full w-full object-cover"
                        />
                    </div>

                    <div className="min-w-0">
                        <p className="font-semibold text-slate-950">
                            {name}
                        </p>

                        <p className="mt-0.5 text-xs text-slate-500">
                            {count} varer
                        </p>
                    </div>
                </div>

                {missing > 0 ? (
                    <span className="shrink-0 rounded-full bg-rose-100 px-2.5 py-1 text-xs font-medium text-rose-700">
                        {missing} utsolgt
                    </span>
                ) : (
                    <span className="shrink-0 rounded-full bg-emerald-100 px-2.5 py-1 text-xs font-medium text-emerald-700">
                        Alt ok
                    </span>
                )}
            </div>

            <div className="relative mt-5 flex items-center justify-between text-xs">
                <span
                    className={`font-medium transition-opacity ${active
                            ? "text-slate-700 opacity-100"
                            : "text-slate-400 opacity-0 group-hover:opacity-100"
                        }`}
                >
                    {active ? "Valgt kategori" : "Klikk for å vise"}
                </span>

                <span
                    className={`text-lg transition-transform duration-200 ${active
                            ? "translate-x-1 text-slate-700"
                            : "text-slate-400 group-hover:translate-x-1"
                        }`}
                >
                    →
                </span>
            </div>
        </button>
    );
}