export type InventoryItem = {
  id: number;
  name: string;
  price: number;
  quantity: number;
  stockValue: number;
  isOrdered: boolean;
  isMissing: boolean;
  missingSinceDate: string | null;
  orderedAtDate: string | null;
  expectedDeliveryDate: string | null;
  notes: string | null;
};

export type InventoryCategory = {
  id: number;
  name: string;
  items: InventoryItem[];
};

export type InventorySummary = {
  totalItems: number;
  totalQuantity: number;
  missingCount: number;
  orderedCount: number;
  totalStockValue: number;
};

export type LowStockAlert = {
  itemId: number;
  itemName: string;
  quantity: number;
  threshold: number;
  message: string;
};

export type InventoryResponse = {
  categories: InventoryCategory[];
  summary: InventorySummary;
  alerts: LowStockAlert[];
};

export type SalesLogEntry = {
  id: number;
  itemId: number;
  itemName: string;
  quantitySold: number;
  unitPrice: number;
  total: number;
  soldAt: string;
};

export type SalesLogResponse = {
  period: string;
  from: string;
  to: string;
  totalUnitsSold: number;
  totalRevenue: number;
  entries: SalesLogEntry[];
};
