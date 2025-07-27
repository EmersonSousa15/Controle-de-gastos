import { useQuery } from "@tanstack/react-query";
import { axiosInstance } from "../service/api";

const fetchPersons = async () => {
  const { data } = await axiosInstance.get("/api/person");
  return data;
};

export const usePersonData = () => {
  return useQuery({
    queryKey: ["person-data"],
    queryFn: fetchPersons,
    retry: false,
  });
};
