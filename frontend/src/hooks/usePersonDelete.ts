import { useMutation, useQueryClient } from "@tanstack/react-query";
import { axiosInstance } from "../service/api";

const deletePersonApi = async (personId: number) => {
  await axiosInstance.delete(`/api/person/${personId}`);
};

export const usePersonDelete = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deletePersonApi,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["total-transactions"] });
      queryClient.invalidateQueries({ queryKey: ["person-data"] });
      queryClient.invalidateQueries({ queryKey: ["list-person"] });
    },
  });
};
