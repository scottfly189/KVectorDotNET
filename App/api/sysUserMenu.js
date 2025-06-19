import { http } from 'uview-plus'

// 收藏菜单
export const addApi = (data) => http.post('/api/sysUserMenu/add', data)

// 取消收藏菜单
export const deleteUserMenuApi = (data) => http.post('/api/sysUserMenu/deleteUserMenu', data)

// 获取当前用户收藏的菜单集合
export const userMenuListApi = (params) => http.get('/api/sysUserMenu/userMenuList', params)

// 获取当前用户收藏的菜单Id集合
export const userMenuIdListApi = (params) => http.get('/api/sysUserMenu/userMenuIdList', params)

