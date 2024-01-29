export default function(api) {
	return {
		// Get all pieces for user
		async getAllForUser (userId) {
			const response = await api.get(`/piece/${userId}`)
			return response.data
		}
	}
}