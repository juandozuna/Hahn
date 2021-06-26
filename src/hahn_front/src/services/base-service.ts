import axios from 'axios';

export abstract class BaseService {
  client = axios.create({
    baseURL: "https://localhost:5001"
  });
}
