import { useQuery } from "@tanstack/react-query";
import { axiosInstance } from "../service/api";

const fetchBalances = async () => {
  const { data } = await axiosInstance.get("/api/balance");
  return data;
};

export const useListPerson = () => {
  return useQuery({
    queryKey: ["list-person"],
    queryFn: fetchBalances,
    retry: false,
  });
};
