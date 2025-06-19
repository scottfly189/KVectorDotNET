import { http } from 'uview-plus'

// APIJSON统一查询
export const Api = (data) => http.post('/api/aPIJSON/get', data)

// APIJSON查询
export const byTableApi = (data) => http.post('/api/aPIJSON/get', data)

// APIJSON新增
export const Api = (data) => http.post('/api/aPIJSON/add', data)

// APIJSON更新
export const editApi = (data) => http.post('/api/aPIJSON/update', data)

// APIJSON删除
export const Api = (data) => http.post('/api/aPIJSON/delete', data)

