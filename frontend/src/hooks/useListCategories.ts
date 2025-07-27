import { useQuery } from "@tanstack/react-query";
import { Category } from "../types/Category";
import { axiosInstance } from "../service/api";

export const useCategories = () => {
  return useQuery<Category[]>({
    queryKey: ["categories"],
    queryFn: () => axiosInstance.get("/api/category").then(res => res.data),
  });
};
