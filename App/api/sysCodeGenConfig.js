import { http } from 'uview-plus'

// 获取代码生成配置列表
export const listApi = (params) => http.get('/api/sysCodeGenConfig/list', params)

// 更新代码生成配置
export const updateApi = (data) => http.post('/api/sysCodeGenConfig/update', data)

// 获取代码生成配置详情
export const detailApi = (params) => http.get('/api/sysCodeGenConfig/detail', params)

