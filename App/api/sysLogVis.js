import { http } from 'uview-plus'

// 获取访问日志分页列表
export const pageApi = (data) => http.post('/api/sysLogVis/page', data)

// 清空访问日志
export const clearApi = (data) => http.post('/api/sysLogVis/clear', data)

// 获取访问日志列表
export const listApi = (params) => http.get('/api/sysLogVis/list', params)

// 按年按天数统计消息日志
export const yearDayStatsApi = (params) => http.get('/api/sysLogVis/yearDayStats', params)

