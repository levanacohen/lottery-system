const { createApp } = Vue;

// 🔗 API Service
const lotteryApi = {
    baseURL: 'http://localhost:5234/api/lottery',
    
    async generateSingle(min = 1, max = 100) {
        const response = await axios.get(`${this.baseURL}/single`, {
            params: { min, max }
        });
        return response.data;
    },

    async generateMultiple(count = 6, min = 1, max = 37, unique = true) {
        const response = await axios.get(`${this.baseURL}/multiple`, {
            params: { count, min, max, unique }
        });
        return response.data;
    },

    async generateCustom(request) {
        const response = await axios.post(`${this.baseURL}/custom`, request);
        return response.data;
    }
};

// 🎯 Vue Application
createApp({
    data() {
        return {
            // UI state
            loading: false,
            error: null,
            
            // Single number
            singleMin: 1,
            singleMax: 100,
            singleResult: null,
            
            // Quick Lotto (fixed settings)
            quickLottoResult: null,
            
            // Custom draw (fully flexible)
            customCount: 7,
            customMin: 1,
            customMax: 37,
            customUnique: true,
            customResult: null,
            
            // Draw history
            history: []
        }
    },

    methods: {
        // Draw a single number
        async generateSingle() {
            this.loading = true;
            this.error = null;
            
            try {
                const result = await lotteryApi.generateSingle(this.singleMin, this.singleMax);
                result.type = 'מספר יחיד';
                this.singleResult = result;
                this.addToHistory(result);
            } catch (error) {
                this.error = 'שגיאה בהגרלת מספר יחיד: ' + (error.response?.data || error.message);
            } finally {
                this.loading = false;
            }
        },
        
        // Quick Lotto with fixed settings
        async generateQuickLotto() {
            this.loading = true;
            this.error = null;
            
            try {
                // תמיד 6 מספרים בין 1-37, ייחודיים
                const result = await lotteryApi.generateMultiple(6, 1, 37, true);
                result.type = 'לוטו מהיר';
                this.quickLottoResult = result;
                this.addToHistory(result);
            } catch (error) {
                this.error = 'שגיאה בהגרלת לוטו מהיר: ' + (error.response?.data || error.message);
            } finally {
                this.loading = false;
            }
        },
        
        // Fully customized draw
        async generateCustom() {
            this.loading = true;
            this.error = null;
            
            try {
                const result = await lotteryApi.generateCustom({
                    count: this.customCount,
                    min: this.customMin,
                    max: this.customMax,
                    unique: this.customUnique
                });
                result.type = 'מותאמת אישית';
                this.customResult = result;
                this.addToHistory(result);
            } catch (error) {
                this.error = 'שגיאה בהגרלה מותאמת: ' + (error.response?.data || error.message);
            } finally {
                this.loading = false;
            }
        },
        
        // Add result to history
        addToHistory(result) {
            this.history.push({
                ...result,
                id: Date.now()
            });
            
            // Keep only the last 10 results
            if (this.history.length > 10) {
                this.history = this.history.slice(-10);
            }
        },
        
        clearHistory() {
            this.history = [];
        },
        
        // Format date and time
        formatTime(dateString) {
            const date = new Date(dateString);
            return date.toLocaleString('he-IL', {
                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit',
                day: '2-digit',
                month: '2-digit'
            });
        }
    },
    
    // App mounted
    mounted() {
        console.log('🎲 מגריל המספרים נטען בהצלחה!');
        console.log('🔗 מתחבר לשרת:', lotteryApi.baseURL);
    }
}).mount('#app');