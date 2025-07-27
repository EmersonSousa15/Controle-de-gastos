import { useQuery } from "@tanstack/react-query";
import { axiosInstance } from "../service/api";
import { Balance } from "../types/Balance";

const fetchTotalTransactions = async (): Promise<Balance> => {
  const { data } = await axiosInstance.get<Balance>("/api/balance/total");
  return data;
};

export const useSumAll = () => {
  const { data, error, isLoading } = useQuery({
    queryKey: ["total-transactions"],
    queryFn: fetchTotalTransactions,
  });

  return { data, error, isLoading };
};
