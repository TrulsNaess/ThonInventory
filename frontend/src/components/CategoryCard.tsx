type Props = {
    name: string;
    count: number;
    missing: number;
    active: boolean;
    onClick: () => void;
};

const categoryStyles: Record<
    string,
    {
        card: string;
        active: string;
        icon: string;
    }
> = {
    BRUS: {
        card: "border-blue-200 bg-gradient-to-br from-blue-50 via-white to-blue-50/40 hover:border-blue-400 hover:shadow-blue-100",
        active: "border-blue-400 ring-2 ring-blue-200 shadow-lg shadow-blue-100",
        icon: "bg-blue-100 text-blue-600",
    },
    SNACKS: {
        card: "border-emerald-200 bg-gradient-to-br from-emerald-50 via-white to-emerald-50/40 hover:border-emerald-400 hover:shadow-emerald-100",
        active: "border-emerald-400 ring-2 ring-emerald-200 shadow-lg shadow-emerald-100",
        icon: "bg-emerald-100 text-emerald-600",
    },
    ALKOHOL: {
        card: "border-violet-200 bg-gradient-to-br from-violet-50 via-white to-violet-50/40 hover:border-violet-400 hover:shadow-violet-100",
        active: "border-violet-400 ring-2 ring-violet-200 shadow-lg shadow-violet-100",
        icon: "bg-violet-100 text-violet-600",
    },
    KIOSK: {
        card: "border-orange-200 bg-gradient-to-br from-orange-50 via-white to-orange-50/40 hover:border-orange-400 hover:shadow-orange-100",
        active: "border-orange-400 ring-2 ring-orange-200 shadow-lg shadow-orange-100",
        icon: "bg-orange-100 text-orange-600",
    },
};

function CategoryIcon({ name }: { name: string }) {
    if (name === "BRUS") {
        return (
            <svg viewBox="0 0 24 24" className="h-6 w-6" fill="none" stroke="currentColor" strokeWidth="1.8">
                <path d="M8 3h8l-1 4v13H9V7L8 3Z" />
                <path d="M9 8h6" />
            </svg>
        );
    }

    if (name === "SNACKS") {
        return (
            <svg viewBox="0 0 24 24" className="h-6 w-6" fill="none" stroke="currentColor" strokeWidth="1.8">
                <path d="M7 4h10l1 16H6L7 4Z" />
                <path d="M9 8c2 1 4 1 6 0" />
            </svg>
        );
    }

    if (name === "ALKOHOL") {
        return (
            <svg viewBox="0 0 24 24" className="h-6 w-6" fill="none" stroke="currentColor" strokeWidth="1.8">
                <path d="M10 3h4v5l3 4v8H7v-8l3-4V3Z" />
                <path d="M8 13h8" />
            </svg>
        );
    }

    return (
        <svg viewBox="0 0 24 24" className="h-6 w-6" fill="none" stroke="currentColor" strokeWidth="1.8">
            <path d="M6 8h12l1 12H5L6 8Z" />
            <path d="M9 9V6a3 3 0 0 1 6 0v3" />
        </svg>
    );
}

export default function CategoryCard({
    name,
    count,
    missing,
    active,
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
                <div className="flex items-center gap-3">
                    <div
                        className={`
              flex h-11 w-11 items-center justify-center rounded-xl
              transition-transform duration-200
              group-hover:scale-110
              ${styles.icon}
            `}
                    >
                        <CategoryIcon name={name} />
                    </div>

                    <div>
                        <p className="font-semibold text-slate-950">{name}</p>
                        <p className="mt-0.5 text-xs text-slate-500">
                            {count} varer
                        </p>
                    </div>
                </div>

                {missing > 0 ? (
                    <span className="rounded-full bg-rose-100 px-2.5 py-1 text-xs font-medium text-rose-700">
                        {missing} utsolgt
                    </span>
                ) : (
                    <span className="rounded-full bg-emerald-100 px-2.5 py-1 text-xs font-medium text-emerald-700">
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
                    className={`text-lg transition-transform duration-200 ${active ? "translate-x-1 text-slate-700" : "text-slate-400 group-hover:translate-x-1"
                        }`}
                >
                    →
                </span>
            </div>
        </button>
    );
}