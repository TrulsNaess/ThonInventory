const API_URL = process.env.NEXT_PUBLIC_API_URL ?? "http://localhost:5003";

export async function apiFetch<T>(path: string, options?: RequestInit): Promise<T> {
  const response = await fetch(`${API_URL}${path}`, {
    cache: "no-store",
    ...options,
  });

  if (!response.ok) {
    throw new Error(`API request failed: ${response.status}`);
  }

  return response.json() as Promise<T>;
}

export async function apiPost<TResponse, TRequest>(path: string, body: TRequest): Promise<TResponse> {
  return apiFetch<TResponse>(path, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(body),
  });
}

export function buildSalesLogPath(period: "today" | "week" | "month", type: "all" | "sale" | "restock" | "shrinkage" = "all") {
  return `/api/sales?period=${period}&type=${type}`;
}
