import { useMutation, useQueryClient } from "@tanstack/react-query";
import { NewCategoryInput } from "../types/Category";
import { axiosInstance } from "../service/api";

const addCategoryApi = (category: NewCategoryInput) => {
  return axiosInstance.post("/api/category", category);
};

export const useCategoryMutate = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: addCategoryApi,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["categories"] });
    },
  });
};
