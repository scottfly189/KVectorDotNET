import { http } from 'uview-plus'

// 获取行政区划分页列表
export const pageApi = (data) => http.post('/api/sysRegion/page', data)

// 获取行政区划列表
export const listApi = (params) => http.get('/api/sysRegion/list', params)

// 查询行政区划列表
export const queryApi = (data) => http.post('/api/sysRegion/query', data)

// 增加行政区划
export const addApi = (data) => http.post('/api/sysRegion/add', data)

// 更新行政区划
export const updateApi = (data) => http.post('/api/sysRegion/update', data)

// 删除行政区划
export const deleteApi = (data) => http.post('/api/sysRegion/delete', data)

// 同步行政区划（民政部）
export const syncRegionMzbApi = (data) => http.post('/api/sysRegion/syncRegionMzb', data)

// 同步行政区划（高德）
export const syncRegionGDApi = (data) => http.post('/api/sysRegion/syncRegionGD', data)

// 同步行政区划数据（国家地名信息库）
export const syncRegionMcaApi = (data) => http.post('/api/sysRegion/syncRegionMca', data)

// 同步行政区划数据（天地图行政区划）
export const syncRegionTiandituApi = (data) => http.post('/api/sysRegion/syncRegionTianditu', data)

// 生成组织架构
export const genOrgApi = (data) => http.post('/api/sysRegion/genOrg', data)

