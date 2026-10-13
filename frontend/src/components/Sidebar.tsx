export default function Sidebar() {
  return (
    <aside className="hidden min-h-screen w-64 shrink-0 flex-col bg-[#00142b] px-5 py-7 text-white md:flex">
      <div className="border-b border-white/10 pb-7">
        <p className="text-[11px] font-semibold tracking-[0.28em] text-slate-300">THON HOTEL</p>
        <h1 className="mt-2 text-xl font-semibold">Lagerstyring</h1>
        <p className="mt-1 text-sm text-slate-400">Kristiansand</p>
      </div>

      <nav className="mt-7 space-y-2 text-sm">
        <div className="rounded-lg bg-white px-4 py-3 font-medium text-[#00142b]">Lager</div>
        <div className="rounded-lg px-4 py-3 text-slate-400">Rapporter</div>
        <div className="rounded-lg px-4 py-3 text-slate-400">Innstillinger</div>
      </nav>

      <div className="mt-auto border-t border-white/10 pt-5 text-xs text-slate-400">
        <p>Thon Hotel Kristiansand</p>
        <p className="mt-1">Resepsjonslager</p>
      </div>
    </aside>
  );
}
