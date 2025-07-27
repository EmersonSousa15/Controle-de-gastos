import { useMutation, useQueryClient } from "@tanstack/react-query";
import { Transaction } from "../types/Transaction";
import { axiosInstance } from "../service/api";

const addTransactionApi = (transaction: Transaction) => {
  return axiosInstance.post("/api/transaction", transaction);
};

export const useTransactionMutate = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: addTransactionApi,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["total-transactions"] });
      queryClient.invalidateQueries({ queryKey: ["list-person"] });
      queryClient.invalidateQueries({ queryKey: ["transactions-of-person"] });
    },
  });
};
