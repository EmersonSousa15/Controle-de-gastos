import { useMutation, useQueryClient } from "@tanstack/react-query";
import { axiosInstance } from "../service/api";

const deleteTransactionApi = (transactionId: number) => {
  return axiosInstance.delete(`/api/transaction/${transactionId}`);
};

export const useDeleteTransaction = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteTransactionApi,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["transactions-of-person"] });
      queryClient.invalidateQueries({ queryKey: ["total-transactions"] });
      queryClient.invalidateQueries({ queryKey: ["list-person"] });
    },
  });
};
