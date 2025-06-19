import { http } from 'uview-plus'

// 获取代码生成模板配置列表
export const listApi = (params) => http.get('/api/sysCodeGenTemplate/list', params)

