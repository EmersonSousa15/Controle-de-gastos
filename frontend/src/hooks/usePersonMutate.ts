import { useMutation, useQueryClient } from "@tanstack/react-query";
import { Person } from "../types/Person";
import { axiosInstance } from "../service/api";

const addPersonApi = (person: Person) => {
  return axiosInstance.post("/api/person", person);
};

export const usePersonMutate = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: addPersonApi,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["person-data"] });
      queryClient.invalidateQueries({ queryKey: ["list-person"] });
    },
  });
};
