import { http } from 'uview-plus'

// 获取消息日志分页列表
export const pageApi = (data) => http.post('/api/sysLogMsg/page', data)

// 获取消息日志详情
export const detailApi = (params) => http.get('/api/sysLogMsg/detail', params)

// 清空消息日志
export const clearApi = (data) => http.post('/api/sysLogMsg/clear', data)

// 按年按天数统计消息日志
export const yearDayStatsApi = (params) => http.get('/api/sysLogMsg/yearDayStats', params)

