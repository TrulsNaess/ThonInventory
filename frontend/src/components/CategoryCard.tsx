type Props = {
  name: string;
  count: number;
  missing: number;
  active: boolean;
  onClick: () => void;
};

export default function CategoryCard({ name, count, missing, active, onClick }: Props) {
  return (
    <button
      type="button"
      onClick={onClick}
      className={`rounded-2xl border p-4 text-left transition ${
        active
          ? "border-[#b9902f] bg-[#fffaf0] shadow-sm"
          : "border-slate-200 bg-white hover:border-slate-300"
      }`}
    >
      <div className="flex items-start justify-between gap-3">
        <div>
          <p className="font-semibold text-slate-900">{name}</p>
          <p className="mt-1 text-xs text-slate-500">{count} varer</p>
        </div>
        {missing > 0 ? (
          <span className="rounded-full bg-rose-50 px-2.5 py-1 text-xs font-medium text-rose-700">
            {missing} mangler
          </span>
        ) : (
          <span className="rounded-full bg-emerald-50 px-2.5 py-1 text-xs font-medium text-emerald-700">
            Alt ok
          </span>
        )}
      </div>
    </button>
  );
}
