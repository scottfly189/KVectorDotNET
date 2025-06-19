import { http } from 'uview-plus'

// 获取OAuth账号列表
export const pageApi = (data) => http.post('/api/sysOAuthUser/page', data)

// 增加OAuth账号
export const addApi = (data) => http.post('/api/sysOAuthUser/add', data)

// 更新OAuth账号
export const updateApi = (data) => http.post('/api/sysOAuthUser/update', data)

// 删除OAuth账号
export const deleteApi = (data) => http.post('/api/sysOAuthUser/delete', data)

