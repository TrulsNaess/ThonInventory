"use client";

import Image from "next/image";
import Link from "next/link";
import { usePathname } from "next/navigation";

const links = [
  { href: "/", label: "Varebeholdning i resepsjon" },
  { href: "/resepsjonslager", label: "Resepsjonslager" },
  { href: "/backoffice-lager", label: "Backoffice lager" },
  { href: "/maintenance-lager", label: "Maintenance lager" },
  { href: "/sales-log", label: "Logg" },
  { href: "/demo-kasse", label: "Demo-Kasse" },
];

export default function Sidebar() {
  const pathname = usePathname();

  return (
    <aside className="hidden min-h-screen w-64 shrink-0 flex-col bg-[#00142b] px-5 py-7 text-white md:flex">
      <div className="border-b border-white/10 pb-7">
        <Image src="/thon-logo-ny.png" alt="Thon Hotel" width={140} height={42} className="h-auto w-auto" priority />
        <h1 className="mt-3 text-xl font-semibold">Lagerstyring</h1>
        <p className="mt-1 text-sm text-slate-400">Kristiansand</p>
      </div>

      <nav className="mt-7 space-y-2 text-sm">
        {links.map((link) => {
          const isActive = pathname === link.href;

          return (
            <Link
              key={link.href}
              href={link.href}
              className={isActive ? "block w-full whitespace-nowrap rounded-lg bg-white px-4 py-3 font-medium text-[#00142b]" : "block w-full whitespace-nowrap rounded-lg px-4 py-3 text-slate-300 hover:bg-white/10"}
            >
              {link.label}
            </Link>
          );
        })}
      </nav>

      <div className="mt-auto border-t border-white/10 pt-5 text-xs text-slate-400">
        <p>Thon Hotel Kristiansand</p>
        <p className="mt-1">Resepsjonslager</p>
      </div>
    </aside>
  );
}
