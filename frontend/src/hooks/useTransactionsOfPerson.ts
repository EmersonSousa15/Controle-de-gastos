import { useQuery } from "@tanstack/react-query";
import { Transaction } from "../types/Transaction";
import { axiosInstance } from "../service/api";

const fetchTransactionsByPerson = (personId: number) => {
  return axiosInstance.get<Transaction[]>(`/api/transaction/person/${personId}`)
    .then(response => response.data);
};

export const useTransactionOfPerson = (personId: number) => {
  return useQuery<Transaction[]>({
    queryKey: ["transactions-of-person", personId],
    queryFn: () => fetchTransactionsByPerson(personId),
    refetchInterval: 60000,
  });
};
