export type InventoryItem = {
  id: number;
  name: string;
  price: number;
  isOrdered: boolean;
  isMissing: boolean;
  missingSinceDate: string | null;
  orderedAtDate: string | null;
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
