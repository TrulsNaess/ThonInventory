import InventoryDashboard from "@/components/InventoryDashboard";
import { apiFetch } from "@/lib/api";
import type { InventoryResponse } from "@/types/inventory";

export default async function Home() {
  try {
    const data = await apiFetch<InventoryResponse>("/api/inventory");
    return <InventoryDashboard data={data} />;
  } catch {
    return <InventoryDashboard data={null} />;
  }
}
