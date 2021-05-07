export interface AccountDetails{
    title: string;
    accountNumber: string;
    availableBalance: number;
    currentBalance: number;
    limitBalance: number;
    category: string;
    currency: string;
    name: string;
    civicAddress: string;
    city: string;
    province: string;
    postalCode: string;
    Country: string;
    phoneNumber: string,
    email: string,
    id: string;
    transactions: Transaction[];
  }

  interface Transaction {
    date: string;
    description: string;
    credit: number;
    debit: number;
    code: string;
    balance: number;
    id: string;
  }