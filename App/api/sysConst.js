import { http } from 'uview-plus'

// 获取所有常量列表
export const listApi = (params) => http.get('/api/sysConst/list', params)

// 根据类名获取常量数据
export const dataApi = (params) => http.get('/api/sysConst/data', params)

