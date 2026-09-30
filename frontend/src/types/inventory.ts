export type InventoryItem = {
  id: number;
  name: string;
  price: number;
  isOrdered: boolean;
  isMissing: boolean;
  notes: string | null;
};

export type InventoryCategory = {
  id: number;
  name: string;
  items: InventoryItem[];
};

export type InventoryResponse = {
  categories: InventoryCategory[];
};
