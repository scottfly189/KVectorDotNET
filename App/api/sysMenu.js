import { http } from 'uview-plus'

// 获取登录菜单树
export const loginMenuTreeApi = (params) => http.get('/api/sysMenu/loginMenuTree', params)

// 获取菜单列表
export const listApi = (params) => http.get('/api/sysMenu/list', params)

// 增加菜单
export const addApi = (data) => http.post('/api/sysMenu/add', data)

// 更新菜单
export const updateApi = (data) => http.post('/api/sysMenu/update', data)

// 删除菜单
export const deleteApi = (data) => http.post('/api/sysMenu/delete', data)

