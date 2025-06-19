import { http } from 'uview-plus'

// 获取角色分页列表
export const pageApi = (data) => http.post('/api/sysRole/page', data)

// 获取角色列表
export const listApi = (params) => http.get('/api/sysRole/list', params)

// 增加角色
export const addApi = (data) => http.post('/api/sysRole/add', data)

// 更新角色
export const updateApi = (data) => http.post('/api/sysRole/update', data)

// 删除角色
export const deleteApi = (data) => http.post('/api/sysRole/delete', data)

// 授权角色菜单
export const grantMenuApi = (data) => http.post('/api/sysRole/grantMenu', data)

// 授权角色表格
export const grantRoleTableApi = (data) => http.post('/api/sysRole/grantRoleTable', data)

// 授权角色数据范围
export const grantDataScopeApi = (data) => http.post('/api/sysRole/grantDataScope', data)

// 授权角色接口
export const grantApiApi = (data) => http.post('/api/sysRole/grantApi', data)

// 设置角色状态
export const setStatusApi = (data) => http.post('/api/sysRole/setStatus', data)

// 获取所有表格字段
export const allTableColumnListApi = (params) => http.get('/api/sysRole/allTableColumnList', params)

// 获取角色表格字段集合
export const roleTableApi = (params) => http.get('/api/sysRole/roleTable', params)

// 获取当前用户表格字段集合
export const userRoleTableListApi = (params) => http.get('/api/sysRole/userRoleTableList', params)

// 根据角色Id获取菜单Id集合
export const ownMenuListApi = (params) => http.get('/api/sysRole/ownMenuList', params)

// 根据角色Id获取机构Id集合
export const ownOrgListApi = (params) => http.get('/api/sysRole/ownOrgList', params)

// 获取角色接口黑名单集合
export const roleApiListApi = (params) => http.get('/api/sysRole/roleApiList', params)

// 获取用户接口集合
export const userApiListApi = (params) => http.get('/api/sysRole/userApiList', params)

