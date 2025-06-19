import { http } from 'uview-plus'

// 获取打印模板列表
export const pageApi = (data) => http.post('/api/sysPrint/page', data)

// 获取打印模板
export const printApi = (params) => http.get('/api/sysPrint/print', params)

// 增加打印模板
export const addApi = (data) => http.post('/api/sysPrint/add', data)

// 更新打印模板
export const updateApi = (data) => http.post('/api/sysPrint/update', data)

// 删除打印模板
export const deleteApi = (data) => http.post('/api/sysPrint/delete', data)

