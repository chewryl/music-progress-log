import axios from 'axios'

import piece from './piece'
import user from './user'

const musicProgressLogAPI = (url) => {
	const api = axios.create({
		baseURL: url,
		responseType: 'json'
	})

	return {
		piece: piece(api),
		user: user(api)
	}
}

export default musicProgressLogAPI(process.env.VUE_APP_MUSIC_PROGRESS_LOG_API_URL)