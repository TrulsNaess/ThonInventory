import type { Metadata } from "next";
import "./globals.css";

export const metadata: Metadata = {
  title: "Thon Inventory",
  description: "Lagerstyring for Thon Hotel Kristiansand",
};

export default function RootLayout({ children }: Readonly<{ children: React.ReactNode }>) {
  return (
    <html lang="no">
      <body>{children}</body>
    </html>
  );
}
