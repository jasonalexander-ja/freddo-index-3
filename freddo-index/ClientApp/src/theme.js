import { 
    createTheme 
} from '@material-ui/core/styles';
import {
    deepPurple,
    yellow
} from '@material-ui/core/colors';

const useTheme = () => {
    return createTheme({
        palette: {
            type: 'dark',
            primary: deepPurple,
            secondary: yellow
        }
    });
};

export default useTheme;
