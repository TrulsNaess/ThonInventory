export type InventoryItem = {
  id: number;
  name: string;
  price: number;
  quantity: number;
  stockValue: number;
  isOrdered: boolean;
  orderedQuantity: number;
  isOrderOverdue: boolean;
  isOutOfStock: boolean;
  isDiscrepancy: boolean;
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
  outOfStockCount: number;
  discrepancyCount: number;
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

export type SalesLogEntryType = "sale" | "restock" | "shrinkage" | "orderplaced" | "orderconfirmed" | "orderrejected";

export type SalesLogCategory = "all" | "given_away" | "misregistered" | "missing_or_stolen" | "orderplaced" | "orderconfirmed" | "orderrejected";

export type SalesLogEntry = {
  id: number;
  itemId: number;
  itemName: string;
  quantity: number;
  unitPrice: number;
  total: number;
  type: SalesLogEntryType;
  category: string;
  note: string | null;
  createdAt: string;
};

export type SalesLogResponse = {
  period: string;
  type: "all" | "sale" | "restock" | "shrinkage" | "order" | "orderplaced" | "orderconfirmed" | "orderrejected";
  category: SalesLogCategory;
  from: string;
  to: string;
  totalUnitsSold: number;
  totalRevenue: number;
  totalShrinkageLoss: number;
  totalRestockValue: number;
  entries: SalesLogEntry[];
};

export type RegisterInventoryChangeRequest = {
  itemId: number;
  quantity: number;
  unitPrice?: number;
  category?: "given_away" | "misregistered" | "missing_or_stolen";
  note?: string;
};

export type RegisterOrderRequest = {
  itemId: number;
  quantity: number;
  expectedDeliveryDate?: string;
  unitPrice?: number;
  note?: string;
};

export type ConfirmOrderRequest = {
  itemId: number;
  receivedQuantity: number;
  note?: string;
};

export type RejectOrderRequest = {
  itemId: number;
  note?: string;
};

export type DemoCheckoutLine = {
  itemId: number;
  quantity: number;
  unitPrice?: number;
};

export type DemoCheckoutRequest = {
  lines: DemoCheckoutLine[];
  note?: string;
};

export type DemoCheckoutResponse = {
  total: number;
  entries: SalesLogEntry[];
};
