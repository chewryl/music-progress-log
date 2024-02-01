export default function(api) {
	return {
		async getAll () {
			const response = await api.get('/user')
			return response.data
		}
	}
}