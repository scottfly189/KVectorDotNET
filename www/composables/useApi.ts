export const useApi = (endPoint: string, language: string = "en", options: any = {}) => {
  const config = useRuntimeConfig();
  const apiUrl = config.public.apiUrl;
  const headers = options.headers || {};
  const method = options.method || 'GET';
  
  // 创建基本请求配置
  const fetchOptions: any = {
    method,
    headers: { ...headers, 'Accept-Language': language },
    onRequest({ request, options }) {
      console.log('Request:', request);
      console.log('Options:', options);
    },
    onResponse({ request, response, options }) {
      console.log('Response:', response._data);
    },
    onResponseError({ request, response, options }) {
      console.error('API Error:', response._data);
    }
  };
  
  // 只有在非 GET 请求时才添加 body
  if (method !== 'GET' && method !== 'HEAD' && options.body) {
    fetchOptions.body = options.body;
  }
  
  return useFetch(`${apiUrl}${endPoint}`, fetchOptions);
}
