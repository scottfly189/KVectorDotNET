import { http } from 'uview-plus'

// 
export const helloWordApi = (params) => http.get('/api/test/helloWord', params)

// 
export const eventTestApi = (data) => http.post('/api/test/eventTest', data)

// 
export const cultureApi = (params) => http.get('/api/test/culture', params)

